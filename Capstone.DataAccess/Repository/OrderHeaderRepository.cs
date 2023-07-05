﻿using Capstone.DataAccess.Data;
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
    }
}
