using System;
using BulkyBook.DataAccess.IRepository;

namespace BulkyBook.DataAccess.Repository
{
	public class UnitOfWork: IUnitOfWork
	{
		public ICategoryRepository Category { get; private set; }
		public ICoverTypeRepository CoverType { get; private set; }
		public IProductRepository Product { get; private set; }
		public ICompanyRepository Company { get; private set; }
		public IShoppingCartRepository ShoppingCart { get; private set; }
		public IApplicationUserRepository ApplicationUser { get; private set; }
        public IOrderDetailRepository OrderDetail { get; private set; }
        public IOrderHeaderRepository OrderHeader { get; private set; }

        private ApplicationDbContext db;

        public UnitOfWork(ApplicationDbContext _db)
        {
            db = _db;
            Category = new CategoryRepository(db);
            CoverType = new CoverTypeRepository(db);
            Product = new ProductRepository(db);
            Company = new CompanyRepository(db);
            ShoppingCart = new ShoppingCartRepository(db);
            ApplicationUser = new ApplicationUserRepository(db);
            OrderDetail = new OrderDetailRepository(db);
            OrderHeader = new OrderHeaderRepository(db);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
