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
        // Seeding data, add-migration after seeding
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Needed for Identity or program won't run with old DbContext
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>().HasData
                (
                    new Customer { Id = 1, Name = "Company 1", Address = "123 Street", City = "SomeCity", Province = "British Columbia", PostalCode = "G4W2K0", Country = "Canada", Phone = "111-222-3333", Email = "mymail@mail.com" },
                    new Customer { Id = 2, Name = "Company 2", Address = "1234 Street", City = "Argon", Province = "Ontario", PostalCode = "H4M3D6", Country = "Canada", Phone = "111-225-3243", Email = "guy@mail.com" },
                    new Customer { Id = 3, Name = "Company 3", Address = "12345 Street", City = "Borat", Province = "Quebec", PostalCode = "J4W2R3", Country = "Canada", Phone = "999-222-3333", Email = "dude@mail.com" }
                );

            modelBuilder.Entity<Product>().HasData
                (
                    new Product 
                    { 
                        Id = 1, 
                        Name = "Prod1", 
                        ProductCode = "HMG1", 
                        NetWeight = 100, 
                        GrossWeight = 110, 
                        IsHazardous = true,
                        UNnumber = "UN0072",
                        Price = 5,
                        BulkRate10 = 4,
                        BulkRate100 = 3.5,
                        Inventory = 109,
                        CustomerId = 1,
                        ImageURL = ""
                    },
                    new Product 
                    { 
                        Id = 2, 
                        Name = "Prod2", 
                        ProductCode = "xx111", 
                        NetWeight = 10, 
                        GrossWeight = 13, 
                        IsHazardous = false,
                        UNnumber = null,
                        Price = 55.70,
                        BulkRate10 = 52.34,
                        BulkRate100 = 49.99,
                        Inventory = 1100,
                        CustomerId = 2,
                        ImageURL = ""
                    },
                    new Product 
                    { 
                        Id = 3, 
                        Name = "Prod3", 
                        ProductCode = "saf13", 
                        NetWeight = 1090, 
                        GrossWeight = 1224, 
                        IsHazardous = true,
                        UNnumber = "UN0004",
                        Price = 1000.50,
                        BulkRate10 = 950.77,
                        BulkRate100 = 899.95,
                        Inventory = 55,
                        CustomerId = 3,
                        ImageURL = ""
                    },
                    new Product 
                    { 
                        Id = 4, 
                        Name = "Prod4", 
                        ProductCode = "fdsfds11", 
                        NetWeight = 12, 
                        GrossWeight = 15, 
                        IsHazardous = true, 
                        UNnumber = "UN0004",
                        Price = 25,
                        BulkRate10 = 22.4,
                        BulkRate100 = 20.5,
                        Inventory = 54,
                        CustomerId = 1,
                        ImageURL = ""
                    },
                    new Product 
                    { 
                        Id = 5, 
                        Name = "Prod5", 
                        ProductCode = "fsdgfds123", 
                        NetWeight = 15, 
                        GrossWeight = 22, 
                        IsHazardous = false,
                        UNnumber = null,
                        Price = 10,
                        BulkRate10 = 9,
                        BulkRate100 = 8.75,
                        Inventory = 101,
                        CustomerId = 1,
                        ImageURL = ""
                    },
                    new Product 
                    { 
                        Id = 6, 
                        Name = "Prod6", 
                        ProductCode = "fdsgds1235", 
                        NetWeight = 55, 
                        GrossWeight = 60, 
                        IsHazardous = false,
                        UNnumber = null,
                        Price = 55,
                        BulkRate10 = 49.99,
                        BulkRate100 = 3.5,
                        Inventory = 45,
                        CustomerId = 3,
                        ImageURL = ""
                    }
              );
        }
    }
}
