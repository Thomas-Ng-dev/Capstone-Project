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
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private ApplicationDbContext _database;
        public OrderDetailRepository(ApplicationDbContext db) : base(db)
        {
            _database = db;
        }
        public void Update(OrderDetail OrderDetail)
        {
            _database.OrderDetails.Update(OrderDetail);
        }
    }
}
