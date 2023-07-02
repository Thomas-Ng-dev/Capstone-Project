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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _database;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _database = db;
        }
        public void Update(Product product)
        {
            _database.Products.Update(product);
        }
    }
}
