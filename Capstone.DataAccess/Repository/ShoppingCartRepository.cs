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
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private ApplicationDbContext _database;
        public ShoppingCartRepository(ApplicationDbContext db) : base(db)
        {
            _database = db;
        }
        public void Update(ShoppingCart shoppingCart)
        {
            _database.ShoppingCarts.Update(shoppingCart);
        }
    }
}
