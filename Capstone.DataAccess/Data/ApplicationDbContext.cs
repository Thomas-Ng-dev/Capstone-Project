using Capstone.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Capstone.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        // Create table in sql server, need to add-migration "COMMENT" in npm. Then update-database.
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        // Seeding data, add-migration after seeding
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Needed for Identity or program won't run with old DbContext
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>().HasData
                (
                    new Customer { Id = 1, Name = "Composite One", Address = "123 Street", City = "SomeCity", Province = "British Columbia", PostalCode = "G4W2K0", Country = "Canada", Phone = "111-222-3333", Email = "mymail@mail.com" },
                    new Customer { Id = 2, Name = "AOC Resin", Address = "1234 Street", City = "Argon", Province = "Ontario", PostalCode = "H4M3D6", Country = "Canada", Phone = "111-225-3243", Email = "guy@mail.com" },
                    new Customer { Id = 3, Name = "Some Company", Address = "12345 Street", City = "Borat", Province = "Quebec", PostalCode = "J4W2R3", Country = "Canada", Phone = "999-222-3333", Email = "dude@mail.com" }
                );

            modelBuilder.Entity<Product>().HasData
                (
                    new Product 
                    { 
                        Id = 1, 
                        Name = "Some Product", 
                        ProductCode = "HMG1", 
                        NetWeight = 100, 
                        GrossWeight = 110, 
                        Price = 5,
                        BulkRate10 = 4,
                        BulkRate100 = 3.5,
                        Inventory = 109,
                        CustomerId = 1,
                        ImageURL = "\\images\\product\\image1.jpeg"
                    },
                    new Product 
                    { 
                        Id = 2, 
                        Name = "Whatever Product", 
                        ProductCode = "xx111", 
                        NetWeight = 10, 
                        GrossWeight = 13, 
                        Price = 55.70,
                        BulkRate10 = 52.34,
                        BulkRate100 = 49.99,
                        Inventory = 1100,
                        CustomerId = 2,
                        ImageURL = "\\images\\product\\image2.jpeg"
					},
                    new Product 
                    { 
                        Id = 3, 
                        Name = "Chemical Pail", 
                        ProductCode = "saf13", 
                        NetWeight = 1090, 
                        GrossWeight = 1224, 
                        Price = 1000.50,
                        BulkRate10 = 950.77,
                        BulkRate100 = 899.95,
                        Inventory = 55,
                        CustomerId = 3,
                        ImageURL = "\\images\\product\\image3.jpg"
					},
                    new Product 
                    { 
                        Id = 4, 
                        Name = "Paint Can Black", 
                        ProductCode = "fdsfds11", 
                        NetWeight = 12, 
                        GrossWeight = 15, 
                        Price = 25,
                        BulkRate10 = 22.4,
                        BulkRate100 = 20.5,
                        Inventory = 54,
                        CustomerId = 1,
                        ImageURL = "\\images\\product\\image4.jpg"
                    }
              );
        }
    }
}
