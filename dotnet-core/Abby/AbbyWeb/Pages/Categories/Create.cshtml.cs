using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AbbyWeb.Model;
using AbbyWeb.Data;

namespace AbbyWeb.Pages.Categories
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext db;

        public Category Category { get; set; }

        public CreateModel(ApplicationDbContext _db)
        {
            db = _db;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            if (Category.Name == Category.DisplayOrder.ToString())
            {
                ModelState.AddModelError(string.Empty, "The display order can't exactly match the name!");
            }
            if (ModelState.IsValid)
            {
                await db.Categories.AddAsync(Category);
                await db.SaveChangesAsync();
                TempData["success"] = "Create category success!";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
