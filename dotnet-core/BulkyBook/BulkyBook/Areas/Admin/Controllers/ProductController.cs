using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BulkyBookWeb.Controllers;
[Area("Admin")]
public class ProductController : Controller
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IWebHostEnvironment hostEnvironment;


    public ProductController(IUnitOfWork _unitOfWork, IWebHostEnvironment _hostEnvironment)
    {
        unitOfWork = _unitOfWork;
        hostEnvironment = _hostEnvironment;
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
            CategoryList = unitOfWork.Category.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }),
            CoverTypeList = unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }),
        };

        if (id != null && id != 0)
        {
            productVM.Product = unitOfWork.Product.GetFirstOrDefaultNull(u => u.Id == id);
        }
        return View(productVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(ProductVM productVM, IFormFile? file)
    {

        if (ModelState.IsValid)
        {
            string wwwRootPath = hostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootPath, @"images/products");
                var extension = Path.GetExtension(file.FileName);

                if (productVM.Product.ImageUrl != null)
                {
                    var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                using (var fileStreams = new FileStream(
                    Path.Combine(uploads, fileName + extension),
                    FileMode.Create))
                {
                    file.CopyTo(fileStreams);
                }
                productVM.Product.ImageUrl = @"/images/products/" + fileName + extension;

            }
            if (productVM.Product.Id == 0)
            {
                unitOfWork.Product.Add(productVM.Product);
                TempData["success"] = "Product created successfully";
            }
            else
            {
                unitOfWork.Product.Update(productVM.Product);
                TempData["success"] = "Product updated successfully";
            }
            unitOfWork.Save();
            return RedirectToAction("Index");
        }
        return View(productVM);
    }

    #region API CALLS
    [HttpGet]
    public IActionResult GetAll()
    {
        var productList = unitOfWork.Product.GetAll(includeProps: "Category,CoverType");
        return Json(new { data = productList });
    }

    [HttpDelete, ActionName("Delete")]
    public IActionResult ApiDelete(int? id)
    {
        var product = unitOfWork.Product.GetFirstOrDefaultNull(p => p.Id == id);
        if (product == null)
        {
            return Json(new { success = false, message = "Error when deleting" });
        } else
        {
            var imagePath = Path.Combine(hostEnvironment.WebRootPath, product.ImageUrl.TrimStart('/'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            unitOfWork.Product.Remove(product);
            unitOfWork.Save();
        }
        return Json(new { success = true, message = "Delete successful" });
    }

    #endregion
}
