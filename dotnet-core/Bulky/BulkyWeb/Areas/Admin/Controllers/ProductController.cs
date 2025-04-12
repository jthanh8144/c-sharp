using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bulky.DataAccess.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Bulky.Models.ViewModels;
using NuGet.Packaging.Signing;
using Bulky.Models;
using Stripe;

namespace BulkyWeb.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };

            if (id != null && id != 0)
            {
                productVM.Product = _unitOfWork.Product.GetFirstOrDefaultNull(
                    u => u.Id == id, includeProps: "ProductImages");
            }
            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM, List<IFormFile>? files)
        {

            if (ModelState.IsValid)
            {
                if (productVM.Product?.Id == 0)
                {
                    _unitOfWork.Product.Add(productVM.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(productVM.Product);
                }
                _unitOfWork.Save();

                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (files != null)
                {
                    foreach (IFormFile file in files)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string productPath = @"images/products/product-" + productVM.Product.Id;
                        string finalPath = Path.Combine(wwwRootPath, productPath);

                        if (!Directory.Exists(finalPath)) Directory.CreateDirectory(finalPath);

                        using (var fileStream = new FileStream(
                            Path.Combine(finalPath, fileName),
                            FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                        ProductImage productImage = new()
                        {
                            ImageUrl = @"/" + productPath + @"/" + fileName,
                            ProductId = productVM.Product.Id
                        };

                        if (productVM.Product.ProductImages == null)
                        {
                            productVM.Product.ProductImages = new List<ProductImage>();
                        }
                        productVM.Product.ProductImages.Add(productImage);
                        _unitOfWork.ProductImage.Add(productImage);
                    }
                    _unitOfWork.Product.Update(productVM.Product);
                    _unitOfWork.Save();
                }
                TempData["success"] = "Product created/updated successfully";

                return RedirectToAction("Index");
            }
            return View(productVM);
        }

        public IActionResult DeleteImage(int imageId)
        {
            var image = _unitOfWork.ProductImage.GetFirstOrDefaultNull(i => i.Id == imageId);
            int productId = image.ProductId;
            if (image != null)
            {
                if (!String.IsNullOrEmpty(image.ImageUrl))
                {
                    var imagePath = Path.Combine(_hostEnvironment.WebRootPath, image.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                    _unitOfWork.ProductImage.Remove(image);
                    _unitOfWork.Save();
                    TempData["success"] = "Deleted successfully";
                }
            }
            return RedirectToAction(nameof(Upsert), new { id = productId });
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll(includeProps: "Category,CoverType");
            return Json(new { data = productList });
        }

        [HttpDelete, ActionName("Delete")]
        public IActionResult ApiDelete(int? id)
        {
            var product = _unitOfWork.Product.GetFirstOrDefaultNull(p => p.Id == id);
            if (product == null)
            {
                return Json(new { success = false, message = "Error when deleting" });
            }
            else
            {
                string productPath = @"images/products/product-" + id;
                string finalPath = Path.Combine(_hostEnvironment.WebRootPath, productPath);
                if (Directory.Exists(finalPath))
                {
                    string[] filePaths = Directory.GetFiles(finalPath);
                    foreach (var filePath in filePaths)
                    {
                        System.IO.File.Delete(filePath);
                    }
                    Directory.Delete(finalPath);
                }
                _unitOfWork.Product.Remove(product);
                _unitOfWork.Save();
            }
            return Json(new { success = true, message = "Delete successful" });
        }

        #endregion
    }
}
