using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bulky.Models;
using Bulky.DataAccess.IRepository;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BulkyWeb.Controllers;

[Area("Customer")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork unitOfWork;

    public HomeController(ILogger<HomeController> logger, IUnitOfWork _unitOfWork)
    {
        _logger = logger;
        unitOfWork = _unitOfWork;
    }

    public IActionResult Index()
    {
        IEnumerable<Product> products = unitOfWork.Product.GetAll(includeProps: "Category,CoverType,ProductImages");
        return View(products);
    }

    public IActionResult Details(int productId)
    {
        ShoppingCart shoppingCart = new()
        {
            Count = 1,
            ProductId = productId,
            Product = unitOfWork.Product.GetFirstOrDefaultNull(p => p.Id == productId, includeProps: "Category,CoverType,ProductImages")
        };
        return View(shoppingCart);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public IActionResult Details(ShoppingCart shoppingCart)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        shoppingCart.ApplicationUserId = claim.Value;

        ShoppingCart? cart = unitOfWork.ShoppingCart.GetFirstOrDefaultNull(
            s => s.ApplicationUserId == claim.Value && s.ProductId == shoppingCart.ProductId);

        if (cart == null)
        {
            unitOfWork.ShoppingCart.Add(shoppingCart);
            unitOfWork.Save();
            HttpContext.Session.SetInt32(
                SD.SessionCart,
                unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == claim.Value).ToList().Count);
        }
        else
        {
            unitOfWork.ShoppingCart.IncrementCount(cart, shoppingCart.Count);
            unitOfWork.Save();
        }
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
