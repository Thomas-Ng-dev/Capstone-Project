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
        public IShoppingCartRepository ShoppingCart { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IOrderHeaderRepository OrderHeader { get; private set; }
        public IOrderDetailRepository OrderDetail { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _database = db;
            Customer = new CustomerRepository(_database);
            Product = new ProductRepository(_database);
            ShoppingCart = new ShoppingCartRepository(_database);
            ApplicationUser = new ApplicationUserRepository(_database);
            OrderHeader = new OrderHeaderRepository(_database);
            OrderDetail = new OrderDetailRepository(_database);
        }

        public void Save()
        {
            _database.SaveChanges();
        }
    }
}
