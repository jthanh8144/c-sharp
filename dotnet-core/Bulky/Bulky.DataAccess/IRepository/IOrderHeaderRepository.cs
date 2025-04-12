using System;
using Bulky.Models;

namespace Bulky.DataAccess.IRepository
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
	{
		void Update(OrderHeader orderHeader);

		void UpdateStatus(int id, string orderStatus, string? paymentStatus = null);

        public void UpdateStripePaymentId(int id, string sessionId, string paymentIntentId);

    }
}
