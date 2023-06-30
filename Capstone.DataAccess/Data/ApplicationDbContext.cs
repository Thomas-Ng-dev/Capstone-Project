using Capstone.Models;
using Microsoft.EntityFrameworkCore;

namespace Capstone.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        // Create table in sql server, need to add-migration "COMMENT" in npm. Then update-database.
        public DbSet<Customer> Customers { get; set; }
        // Seeding data, add-migration after seeding
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Name = "Company 1", Address = "123 Street", City = "SomeCity", Region = "Washington Stats", PostalCode = "12345", Country = "United States", Phone = "111-222-3333", Email = "mymail@mail.com" },
                new Customer { Id = 2, Name = "Company 2", Address = "1234 Street", City = "Argon", Region = "New York", PostalCode = "45678", Country = "United States", Phone = "111-225-3243", Email = "guy@mail.com" },
                new Customer { Id = 3, Name = "Company 3", Address = "12345 Street", City = "Borat", Region = "Quebec", PostalCode = "J4W2R3", Country = "Canada", Phone = "999-222-3333", Email = "dude@mail.com" }
                );
        }
    }
}
