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
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext db;

        public Category Category { get; set; }

        public DeleteModel(ApplicationDbContext _db)
        {
            db = _db;
        }

        public void OnGet(int? id)
        {
            Category = db.Categories.Find(id);
        }

        public async Task<IActionResult> OnPost(int? id)
        {
            if (ModelState.IsValid)
            {
                var data = db.Categories.Find(id);
                if (data != null)
                {
                    db.Categories.Remove(data);
                    db.SaveChanges();
                    TempData["success"] = "Delete category success!";
                    return RedirectToPage("Index");
                }
            }
            return Page();
        }
    }
}
