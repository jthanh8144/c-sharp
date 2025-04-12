using Bulky.DataAccess;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bulky.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Bulky.Utility;
using Microsoft.AspNetCore.Identity;

namespace BulkyBookWeb.Controllers;
[Area("Admin")]
public class UserController : Controller
{
    //private readonly ApplicationDbContext _db;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;

    public UserController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult RoleManagement(string userId)
    {
        RoleManagementVM RoleVM = new RoleManagementVM()
        {
            ApplicationUser = _unitOfWork.ApplicationUser
                .GetFirstOrDefaultNull(u => u.Id == userId, includeProps: "Company"),
            RoleList = _roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Name
            }),
            CompanyList = _unitOfWork.Company.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            })
        };
        RoleVM.ApplicationUser.Role = _userManager.GetRolesAsync(
            _unitOfWork.ApplicationUser.GetFirstOrDefaultNull(u => u.Id == userId)
        ).GetAwaiter().GetResult().FirstOrDefault();
        return View(RoleVM);
    }

    [HttpPost]
    public IActionResult RoleManagement(RoleManagementVM roleVM)
    {
        string oldRole = _userManager.GetRolesAsync(
            _unitOfWork.ApplicationUser.GetFirstOrDefaultNull(u => u.Id == roleVM.ApplicationUser.Id)
        ).GetAwaiter().GetResult().FirstOrDefault();
        ApplicationUser applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefaultNull(u => u.Id == roleVM.ApplicationUser.Id);
        
        if (roleVM.ApplicationUser.Role != oldRole)
        {
            if (roleVM.ApplicationUser.Role == SD.RoleUserComp)
            {
                applicationUser.CompanyId = roleVM.ApplicationUser.CompanyId;
            }
            if (oldRole == SD.RoleUserComp)
            {
                applicationUser.CompanyId = null;
            }
            _unitOfWork.ApplicationUser.Update(applicationUser);
            _unitOfWork.Save();
            _userManager.RemoveFromRoleAsync(applicationUser, oldRole).GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(applicationUser, roleVM.ApplicationUser.Role).GetAwaiter().GetResult();
        }
        else
        {
            if (oldRole == SD.RoleUserComp && applicationUser.CompanyId != roleVM.ApplicationUser.CompanyId)
            {
                applicationUser.CompanyId = roleVM.ApplicationUser.CompanyId;
                _unitOfWork.ApplicationUser.Update(applicationUser);
                _unitOfWork.Save();
            }
        }
        return RedirectToAction("Index");
    }

    #region API CALLS
    [HttpGet]
    public IActionResult GetAll()
    {
        List<ApplicationUser> users = _unitOfWork.ApplicationUser.GetAll(includeProps: "Company").ToList();

        foreach (var user in users)
        {
            user.Role = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().FirstOrDefault();
            if (user.Company == null)
            {
                user.Company = new Company { Name = "" };
            }
        }
        return Json(new { data = users });
    }

    [HttpPost]
    public IActionResult LockUnlock([FromBody] string id)
    {
        var user = _unitOfWork.ApplicationUser.GetFirstOrDefaultNull(u => u.Id == id);
        if (user == null)
        {
            return Json(new { success = false, message = "Error when locking/unlocking" });
        }
        if (user.LockoutEnd != null && user.LockoutEnd > DateTime.Now)
        {
            user.LockoutEnd = DateTime.Now;
        }
        else
        {
            user.LockoutEnd = DateTime.Now.AddYears(1000);
        }
        _unitOfWork.ApplicationUser.Update(user);
        _unitOfWork.Save();
        return Json(new { success = true, message = "Locking/Unlocking success" });
    }
    #endregion
}
