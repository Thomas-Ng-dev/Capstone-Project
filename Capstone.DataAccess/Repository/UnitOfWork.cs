using Capstone.DataAccess.Data;
using Capstone.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _database;
        // Pass all models within this interface
        public ICustomerRepository Customer { get; private set; }
        public IProductRepository Product { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _database = db;
            Customer = new CustomerRepository(_database);
            Product = new ProductRepository(_database);
        }

        public void Save()
        {
            _database.SaveChanges();
        }
    }
}
