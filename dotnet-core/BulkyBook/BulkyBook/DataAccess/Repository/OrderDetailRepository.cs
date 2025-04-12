using System;
using BulkyBook.Models;
using BulkyBook.DataAccess.IRepository;

namespace BulkyBook.DataAccess.Repository
{
	public class OrderDetailRepository: Repository<OrderDetail>, IOrderDetailRepository
	{
        private ApplicationDbContext db;

		public OrderDetailRepository(ApplicationDbContext _db) : base(_db)
		{
            db = _db;
		}

        public void Update(OrderDetail orderDetail)
        {
            db.OrderDetails.Update(orderDetail);
        }
    }
}

