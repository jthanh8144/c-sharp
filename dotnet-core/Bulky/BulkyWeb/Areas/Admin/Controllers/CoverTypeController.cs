using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bulky.DataAccess;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Bulky.DataAccess.IRepository;

namespace Bulky.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CoverTypeController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<CoverType> objCoverTypeList = unitOfWork.CoverType.GetAll();
            return View(objCoverTypeList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.CoverType.Add(coverType);
                unitOfWork.Save();
                TempData["success"] = "Cover type created successfully!";
                return RedirectToAction("Index"); 
            }
            return View(coverType);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //Category category = db.Categories.Find(id);
            CoverType coverType = unitOfWork.CoverType.GetFirstOrDefaultNull(c => c.Id == id);
            if (coverType == null)
            {
                return NotFound();
            }
            return View(coverType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.CoverType.Update(coverType);
                unitOfWork.Save();
                TempData["success"] = "Cover type updated successfully!";
                return RedirectToAction("Index");
            }
            return View(coverType);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //Category category = db.Find(id);
            CoverType coverType = unitOfWork.CoverType.GetFirstOrDefaultNull(c => c.Id == id);

            if (coverType == null)
            {
                return NotFound();
            }
            return View(coverType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteHandle(int? id)
        {
            //var category = db.Categories.Find(id);
            CoverType coverType = unitOfWork.CoverType.GetFirstOrDefaultNull(c => c.Id == id);

            if (coverType == null)
            {
                return NotFound();
            }
            unitOfWork.CoverType.Remove(coverType);
            unitOfWork.Save();
            TempData["success"] = "Cover type deleted successfully!";
            return RedirectToAction("Index");
        }
    } 
}
