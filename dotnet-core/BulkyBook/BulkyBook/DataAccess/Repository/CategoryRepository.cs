using System;
using BulkyBook.Models;
using BulkyBook.DataAccess.IRepository;

namespace BulkyBook.DataAccess.Repository
{
	public class CategoryRepository: Repository<Category>, ICategoryRepository
	{
        private ApplicationDbContext db;

		public CategoryRepository(ApplicationDbContext _db) : base(_db)
		{
            db = _db;
		}

        //public void Save()
        //{
        //    db.SaveChanges();
        //}

        public void Update(Category category)
        {
            db.Categories.Update(category);
        }
    }
}

