using CapstoneProject.Models;
using Microsoft.EntityFrameworkCore;

namespace CapstoneProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        // Create table in sql server, need to add-migration "COMMENT" in npm. Then update-database.
        public DbSet<Customer> Customers { get; set; }
    }
}
