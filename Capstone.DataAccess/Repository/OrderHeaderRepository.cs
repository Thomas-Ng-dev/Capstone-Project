using Capstone.DataAccess.Data;
using Capstone.DataAccess.Repository.IRepository;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private ApplicationDbContext _database;
        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _database = db;
        }
        public void Update(OrderHeader OrderHeader)
        {
            _database.OrderHeaders.Update(OrderHeader);
        }

		public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
		{
			var orderFromDb = _database.OrderHeaders.FirstOrDefault(x => x.Id == id);
            if (orderFromDb != null) 
            {
                orderFromDb.OrderStatus = orderStatus;
                if(!string.IsNullOrEmpty(paymentStatus))
                {
                    orderFromDb.PaymentStatus = paymentStatus;
                }
            }
		}

		public void UpdateStripePaymentId(int id, string sessionId, string paymentIntentId)
		{
			var orderFromDb = _database.OrderHeaders.FirstOrDefault(x => x.Id == id);
            if (!string.IsNullOrEmpty(sessionId))
            {
                orderFromDb.SessionId = sessionId;
            }
			if (!string.IsNullOrEmpty(paymentIntentId))
			{
                orderFromDb.PaymentIntentId = paymentIntentId;
			}
		}
	}
}
