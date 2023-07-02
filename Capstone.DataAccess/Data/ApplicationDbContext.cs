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
        public DbSet<Product> Products { get; set; }
        // Seeding data, add-migration after seeding
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Name = "Company 1", Address = "123 Street", City = "SomeCity", Region = "Washington Stats", PostalCode = "12345", Country = "United States", Phone = "111-222-3333", Email = "mymail@mail.com" },
                new Customer { Id = 2, Name = "Company 2", Address = "1234 Street", City = "Argon", Region = "New York", PostalCode = "45678", Country = "United States", Phone = "111-225-3243", Email = "guy@mail.com" },
                new Customer { Id = 3, Name = "Company 3", Address = "12345 Street", City = "Borat", Region = "Quebec", PostalCode = "J4W2R3", Country = "Canada", Phone = "999-222-3333", Email = "dude@mail.com" }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Prod1", ProductCode = "HMG1", NetWeight = 100, GrossWeight = 110, IsHazardous = true, Inventory = 10 },
                new Product { Id = 2, Name = "Prod2", ProductCode = "xx111", NetWeight = 10, GrossWeight = 13, IsHazardous = false, Inventory = 11 },
                new Product { Id = 3, Name = "Prod3", ProductCode = "saf13", NetWeight = 1090, GrossWeight = 1224, IsHazardous = true, Inventory = 1 },
                new Product { Id = 4, Name = "Prod4", ProductCode = "fdsfds11", NetWeight = 12, GrossWeight = 15, IsHazardous = true, Inventory = 54 },
                new Product { Id = 5, Name = "Prod5", ProductCode = "fsdgfds123", NetWeight = 15, GrossWeight = 22, IsHazardous = false, Inventory = 101 },
                new Product { Id = 6, Name = "Prod6", ProductCode = "fdsgds1235", NetWeight = 55, GrossWeight = 60, IsHazardous = false, Inventory = 45 }
                );
        }
    }
}
