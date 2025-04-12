using System;
using Bulky.Models;
using Bulky.DataAccess.IRepository;
using Bulky.DataAccess.Data;

namespace Bulky.DataAccess.Repository
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

