using System;
using Bulky.DataAccess.Data;
using Bulky.DataAccess.IRepository;
using Bulky.Models;

namespace Bulky.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
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
                productInDb.ProductImages = product.ProductImages;
            }
        }
    }
}
