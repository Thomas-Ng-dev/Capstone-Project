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
            var productFromDB = _database.Products.FirstOrDefault(x =>  x.Id == product.Id);
            if (productFromDB != null)
            {
                productFromDB.Name = product.Name;
                productFromDB.ProductCode = product.ProductCode;
                productFromDB.NetWeight = product.NetWeight;
                productFromDB.GrossWeight = product.GrossWeight;
                productFromDB.IsHazardous = product.IsHazardous;
                productFromDB.UNnumber = product.UNnumber;
                productFromDB.Price = product.Price;
                productFromDB.BulkRate10 = product.BulkRate10;
                productFromDB.BulkRate100 = product.BulkRate100;
                productFromDB.Inventory = product.Inventory;
                productFromDB.CustomerId = product.CustomerId;
                if(product.ImageURL != null)
                {
                    productFromDB.ImageURL = product.ImageURL;
                }
            }
        }
    }
}
