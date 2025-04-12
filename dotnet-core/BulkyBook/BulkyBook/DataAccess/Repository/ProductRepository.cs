using System;
using BulkyBook.Models;
using BulkyBook.DataAccess.IRepository;

namespace BulkyBook.DataAccess.Repository
{
	public class ProductRepository: Repository<Product>, IProductRepository
	{
        private ApplicationDbContext db;

		public ProductRepository(ApplicationDbContext _db) : base(_db)
		{
            db = _db;
		}

        public void Update(Product product)
        {
            var productInDb = db.Products.FirstOrDefault(p => p.Id == product.Id);
            if (product != null)
            {
                productInDb.Title = product.Title;
                productInDb.Description = product.Description;
                productInDb.ISBN = product.ISBN;
                productInDb.Author = product.Author;
                productInDb.ListPrice = product.ListPrice;
                productInDb.Price = product.Price;
                productInDb.Price50 = product.Price50;
                productInDb.Price100 = product.Price100;
                productInDb.CategoryId = product.CategoryId;
                productInDb.CoverTypeId = product.CoverTypeId;
                if (product.ImageUrl != null)
                {
                    productInDb.ImageUrl = product.ImageUrl;
                }
            }
        }
    }
}

