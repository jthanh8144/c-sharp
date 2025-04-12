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
public class CompanyController : Controller
{
    private readonly IUnitOfWork unitOfWork;


    public CompanyController(IUnitOfWork _unitOfWork)
    {
        unitOfWork = _unitOfWork;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Upsert(int? id)
    {
        Company company = new();
        if (id != null && id != 0)
        {
            company = unitOfWork.Company.GetFirstOrDefaultNull(c => c.Id == id);
        }
        return View(company);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(Company company, IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            if (company.Id == 0)
            {
                unitOfWork.Company.Add(company);
                TempData["success"] = "Company created successfully";
            }
            else
            {
                unitOfWork.Company.Update(company);
                TempData["success"] = "Company updated successfully";
            }
            unitOfWork.Save();
            return RedirectToAction("Index");
        }
        return View(company);
    }


    #region API CALLS
    [HttpGet]
    public IActionResult GetAll()
    {
        var companies = unitOfWork.Company.GetAll();
        return Json(new { data = companies });
    }

    [HttpDelete, ActionName("Delete")]
    public IActionResult ApiDelete(int? id)
    {
        var company = unitOfWork.Company.GetFirstOrDefaultNull(c => c.Id == id);
        if (company == null)
        {
            return Json(new { success = false, message = "Error when deleting" });
        } else
        {
            unitOfWork.Company.Remove(company);
            unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful" });
        }
    }

    #endregion
}
