using System;
using BulkyBook.Models;
using BulkyBook.DataAccess.IRepository;

namespace BulkyBook.DataAccess.Repository
{
	public class OrderHeaderRepository: Repository<OrderHeader>, IOrderHeaderRepository
	{
        private ApplicationDbContext db;

		public OrderHeaderRepository(ApplicationDbContext _db) : base(_db)
		{
            db = _db;
		}

        public void Update(OrderHeader orderHeader)
        {
            db.OrderHeaders.Update(orderHeader);
        }

        public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
        {
            var order = db.OrderHeaders.FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                order.OrderStatus = orderStatus;
                if (paymentStatus != null)
                {
                    order.PaymentStatus = paymentStatus;
                }
            }
        }

        public void UpdateStripePaymentId(int id, string sessionId, string paymentIntentId)
        {
            var order = db.OrderHeaders.FirstOrDefault(o => o.Id == id);
            order.PaymentDate = DateTime.Now;
            order.SessionId = sessionId;
            order.PaymentIntentId = paymentIntentId;
        }
    }
}

