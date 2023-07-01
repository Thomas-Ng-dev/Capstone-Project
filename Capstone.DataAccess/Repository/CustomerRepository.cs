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
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private ApplicationDbContext _database;
        public CustomerRepository(ApplicationDbContext db) : base(db)
        {
            _database = db;
        }
        public void Update(Customer customer)
        {
            _database.Customers.Update(customer);
        }
    }
}
