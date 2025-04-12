using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AbbyWeb.Data;
using AbbyWeb.Model;

namespace AbbyWeb.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext db;
        public IEnumerable<Category> Categories { get; set; }

        public IndexModel(ApplicationDbContext _db)
        {
            db = _db;
        }

        public void OnGet()
        {
            Categories = db.Categories;
        }
    }
}
