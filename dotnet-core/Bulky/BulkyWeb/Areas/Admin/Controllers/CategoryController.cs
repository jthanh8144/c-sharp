using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bulky.DataAccess.Data;
using Bulky.Models;
using Bulky.DataAccess.IRepository;
using Microsoft.Extensions.Hosting;

namespace BulkyWeb.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Category> categories = _unitOfWork.Category.GetAll().OrderBy(category => category.DisplayOrder).ToList();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The display order can't exactly match the name");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(category);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully!";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? category = _unitOfWork.Category.GetFirstOrDefaultNull(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Update(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The display order can't exactly match the name");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(category);
                _unitOfWork.Save();
                TempData["success"] = "Category updated successfully!";
                return RedirectToAction("Index");
            }
            return View();
        }

        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Category? category = _unitOfWork.Category.GetFirstOrDefaultNull(c => c.Id == id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(category);
        //}

        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeleteHandle(int? id)
        //{
        //    Category? category = _unitOfWork.Category.GetFirstOrDefaultNull(c => c.Id == id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }
        //    _unitOfWork.Category.Remove(category);
        //    _unitOfWork.Save();
        //    TempData["success"] = "Category deleted successfully!";
        //    return RedirectToAction("Index");
        //}

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var categoryList = _unitOfWork.Category
                .GetAll()
                .OrderBy(category => category.DisplayOrder);
            return Json(new { data = categoryList });
        }

        [HttpDelete, ActionName("Delete")]
        public IActionResult ApiDelete(int? id)
        {
            Category? category = _unitOfWork.Category.GetFirstOrDefaultNull(c => c.Id == id);
            if (category == null)
            {
                return Json(new { success = false, message = "Error when deleting" });
            }
            else
            {
                _unitOfWork.Category.Remove(category);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Delete successful" });
            }
        }
        #endregion
    }
}
