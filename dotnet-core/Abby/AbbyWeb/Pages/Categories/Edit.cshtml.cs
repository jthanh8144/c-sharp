using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbbyWeb.Data;
using AbbyWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext db;

        public Category Category { get; set; }

        public EditModel(ApplicationDbContext _db)
        {
            db = _db;
        }

        public void OnGet(int? id)
        {
            Category = db.Categories.Find(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (Category.Name == Category.DisplayOrder.ToString())
            {
                ModelState.AddModelError(string.Empty, "The display order can't exactly match the name!");
            }
            if (ModelState.IsValid)
            {
                db.Categories.Update(Category);
                await db.SaveChangesAsync();
                TempData["success"] = "Update category success!";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
