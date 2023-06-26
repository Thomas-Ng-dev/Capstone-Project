﻿// <auto-generated />
using CapstoneProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CapstoneProject.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.5.23280.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CapstoneProject.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "123 Street",
                            City = "SomeCity",
                            Country = "United States",
                            Email = "mymail@mail.com",
                            Name = "Company 1",
                            Phone = "111-222-3333",
                            PostalCode = "12345",
                            Region = "Washington Stats"
                        },
                        new
                        {
                            Id = 2,
                            Address = "1234 Street",
                            City = "Argon",
                            Country = "United States",
                            Email = "guy@mail.com",
                            Name = "Company 2",
                            Phone = "111-225-3243",
                            PostalCode = "45678",
                            Region = "New York"
                        },
                        new
                        {
                            Id = 3,
                            Address = "12345 Street",
                            City = "Borat",
                            Country = "Canada",
                            Email = "dude@mail.com",
                            Name = "Company 3",
                            Phone = "999-222-3333",
                            PostalCode = "J4W2R3",
                            Region = "Quebec"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
