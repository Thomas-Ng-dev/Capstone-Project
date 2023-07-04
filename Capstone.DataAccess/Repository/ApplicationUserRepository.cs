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
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private ApplicationDbContext _database;
        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _database = db;
        }
        
        // Not needed for the moment
        //public void Update(ApplicationUser user)
        //{
        //    _database.ApplicationUsers.Update(user);
        //}
    }
}
