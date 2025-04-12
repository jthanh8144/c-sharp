using System;
using System.Security.Claims;
using Bulky.DataAccess.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace Bulky.Areas.Customer.Controllers
{
	[Area("Customer")]
	[Authorize]
	public class CartController : Controller
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IEmailSender emailSender;
        private readonly string? _host;

        [BindProperty]
		public ShoppingCartVM ShoppingCartVM { get; set; }

		public int OrderTotal { get; set; }

		public CartController(IUnitOfWork _unitOfWork, IEmailSender _emailSender, IConfiguration configuration)
		{
			unitOfWork = _unitOfWork;
			emailSender = _emailSender;
            _host = configuration.GetValue<string>("EnvironmentVariables:Host");
        }

        public IActionResult Index()
		{
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

			ShoppingCartVM = new ShoppingCartVM
			{
				ListCart = unitOfWork.ShoppingCart.GetAll(
					s => s.ApplicationUserId == claim.Value, includeProps: "Product"),
				OrderHeader = new ()
			};
            IEnumerable<ProductImage> productImages = unitOfWork.ProductImage.GetAll();
			foreach (var item in ShoppingCartVM.ListCart)
			{
                item.Product.ProductImages = productImages.Where(i => i.ProductId == item.Product.Id).ToList();
				item.Price = GetPriceBasedOnQuantity(item.Count, item.Product.Price, item.Product.Price50, item.Product.Price100);
				ShoppingCartVM.OrderHeader.OrderTotal += (item.Price * item.Count);
			}
            return View(ShoppingCartVM);
		}

		private double GetPriceBasedOnQuantity(double Quantity, double Price, double Price50, double Price100)
		{
			if (Quantity <= 50)
			{
				return Price;
			} else if (Quantity <= 100)
			{
				return Price50;
			} else
			{
				return Price100;
			}
		}

		public IActionResult Plus(int cartId)
		{
			var cart = unitOfWork.ShoppingCart.GetFirstOrDefaultNull(s => s.Id == cartId);
			unitOfWork.ShoppingCart.IncrementCount(cart, 1);
			unitOfWork.Save();
			return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int cartId)
        {
            var cart = unitOfWork.ShoppingCart.GetFirstOrDefaultNull(s => s.Id == cartId);
			if (cart.Count == 1)
			{
                unitOfWork.ShoppingCart.Remove(cart);
                var count = unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == cart.ApplicationUserId).ToList().Count - 1;
                HttpContext.Session.SetInt32(SD.SessionCart, count);
            }
			else
			{
				unitOfWork.ShoppingCart.DecrementCount(cart, 1);
			}
            unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int cartId)
        {
            var cart = unitOfWork.ShoppingCart.GetFirstOrDefaultNull(s => s.Id == cartId);
            unitOfWork.ShoppingCart.Remove(cart);
            unitOfWork.Save();
			var count = unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == cart.ApplicationUserId).ToList().Count;
			HttpContext.Session.SetInt32(SD.SessionCart, count);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Summary()
        {
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

			ShoppingCartVM = new ShoppingCartVM
			{
				ListCart = unitOfWork.ShoppingCart.GetAll(
					s => s.ApplicationUserId == claim.Value, includeProps: "Product"),
				OrderHeader = new()
			};
			ShoppingCartVM.OrderHeader.ApplicationUser
				= unitOfWork.ApplicationUser.GetFirstOrDefaultNull(u => u.Id == claim.Value);
			ShoppingCartVM.OrderHeader.Name = ShoppingCartVM.OrderHeader.ApplicationUser.Name;
			ShoppingCartVM.OrderHeader.PhoneNumber = ShoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
			ShoppingCartVM.OrderHeader.StreetAddress = ShoppingCartVM.OrderHeader.ApplicationUser.StreetAddress;
			ShoppingCartVM.OrderHeader.City = ShoppingCartVM.OrderHeader.ApplicationUser.City;
			ShoppingCartVM.OrderHeader.State = ShoppingCartVM.OrderHeader.ApplicationUser.State;
			ShoppingCartVM.OrderHeader.PostalCode = ShoppingCartVM.OrderHeader.ApplicationUser.PostalCode;

			foreach (var item in ShoppingCartVM.ListCart)
			{
				item.Price = GetPriceBasedOnQuantity(item.Count, item.Product.Price, item.Product.Price50, item.Product.Price100);
				ShoppingCartVM.OrderHeader.OrderTotal += (item.Price * item.Count);
			}
			return View(ShoppingCartVM);
        }

		[HttpPost]
		[ActionName("Summary")]
		[ValidateAntiForgeryToken]
        public IActionResult SubmitSummary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

			ShoppingCartVM.ListCart = unitOfWork.ShoppingCart
				.GetAll(c => c.ApplicationUserId == claim.Value, includeProps: "Product");

			ShoppingCartVM.OrderHeader.OrderDate = DateTime.Now;
			ShoppingCartVM.OrderHeader.ApplicationUserId = claim.Value;

            foreach (var item in ShoppingCartVM.ListCart)
            {
                item.Price = GetPriceBasedOnQuantity(item.Count, item.Product.Price, item.Product.Price50, item.Product.Price100);
                ShoppingCartVM.OrderHeader.OrderTotal += (item.Price * item.Count);
            }

            ApplicationUser applicationUser = unitOfWork.ApplicationUser.GetFirstOrDefaultNull(u => u.Id == claim.Value);
            if (applicationUser.CompanyId.GetValueOrDefault() == 0)
            {
                ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
                ShoppingCartVM.OrderHeader.OrderStatus = SD.StatusPending;
            }
            else
            {
                ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusDelayedPayment;
                ShoppingCartVM.OrderHeader.OrderStatus = SD.StatusApproved;
            }

            unitOfWork.OrderHeader.Add(ShoppingCartVM.OrderHeader);
			unitOfWork.Save();

			foreach (var item in ShoppingCartVM.ListCart)
			{
				OrderDetail orderDetail = new()
				{
					ProductId = item.ProductId,
					OrderId = ShoppingCartVM.OrderHeader.Id,
					Price = item.Price,
					Count = item.Count
				};
				unitOfWork.OrderDetail.Add(orderDetail);
				unitOfWork.Save();
			}
            if (applicationUser.CompanyId.GetValueOrDefault() == 0)
			{
                // Stripe setting
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string>
                {
                    "card"
                },
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                    SuccessUrl = _host + $"customer/cart/OrderConfirmation?id={ShoppingCartVM.OrderHeader.Id}",
                    CancelUrl = _host + $"customer/cart/index",
                };
                foreach (var item in ShoppingCartVM.ListCart)
                {
                    var sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(item.Price * 100),
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Title,
                            }
                        },
                        Quantity = item.Count
                    };
                    options.LineItems.Add(sessionLineItem);
                }
                var service = new SessionService();
                Session session = service.Create(options);
                unitOfWork.OrderHeader.UpdateStripePaymentId(ShoppingCartVM.OrderHeader.Id, session.Id, session.PaymentIntentId);
                unitOfWork.Save();
                Response.Headers.Add("Location", session.Url);
                return new StatusCodeResult(303);
            }
            else
			{
				return RedirectToAction("OrderConfirmation", "Cart", new
				{
					id = ShoppingCartVM.OrderHeader.Id
				});
			}
        }

		public IActionResult OrderConfirmation(int id)
		{
			OrderHeader orderHeader = unitOfWork.OrderHeader.GetFirstOrDefaultNull(o => o.Id == id, includeProps: "ApplicationUser");
			if (orderHeader.PaymentStatus != SD.PaymentStatusDelayedPayment)
            {
                var service = new SessionService();
                Session session = service.Get(orderHeader.SessionId);

                // check the stripe status
                if (session.PaymentStatus.ToLower() == "paid")
                {
                    unitOfWork.OrderHeader.UpdateStripePaymentId(id, orderHeader.SessionId, session.PaymentIntentId);
                    unitOfWork.OrderHeader.UpdateStatus(id, SD.StatusApproved, SD.PaymentStatusApproved);
                    unitOfWork.Save();
                }
            }

			emailSender.SendEmailAsync(orderHeader.ApplicationUser.Email, "New order - Bulky Book", "<p>New order created</p>");

            List<ShoppingCart> shoppingCarts = unitOfWork.ShoppingCart
                .GetAll(c => c.ApplicationUserId == orderHeader.ApplicationUserId).ToList();

			HttpContext.Session.Clear();
            unitOfWork.ShoppingCart.RemoveRange(shoppingCarts);
            unitOfWork.Save();

            return View(id);
        }
    }
}

