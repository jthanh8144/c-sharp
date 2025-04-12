using System;
using System.Security.Claims;
using Bulky.DataAccess.IRepository;
using Bulky.Utility;
using Microsoft.AspNetCore.Mvc;

namespace Bulky.ViewComponents
{
	public class ShoppingCartViewComponent : ViewComponent
	{
		private readonly IUnitOfWork unitOfWork;

		public ShoppingCartViewComponent(IUnitOfWork _unitOfWork)
		{
			unitOfWork = _unitOfWork;
		}

		public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity) User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			if (claim != null)
			{
				if (HttpContext.Session.GetInt32(SD.SessionCart) != null)
				{
					return View(HttpContext.Session.GetInt32(SD.SessionCart));
				}
				else
				{
					HttpContext.Session.SetInt32(
						SD.SessionCart,
						unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == claim.Value).ToList().Count);
                    return View(HttpContext.Session.GetInt32(SD.SessionCart));
                }
			}
			else
			{
				HttpContext.Session.Clear();
				return View(0);
			}
        }
	}
}
