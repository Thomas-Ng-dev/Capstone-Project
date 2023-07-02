using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Capstone.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class createTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NetWeight = table.Column<double>(type: "float", nullable: false),
                    GrossWeight = table.Column<double>(type: "float", nullable: false),
                    IsHazardous = table.Column<bool>(type: "bit", nullable: false),
                    UNnumber = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    BulkRate10 = table.Column<double>(type: "float", nullable: false),
                    BulkRate100 = table.Column<double>(type: "float", nullable: false),
                    Inventory = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "City", "Country", "Email", "Name", "Phone", "PostalCode", "Province" },
                values: new object[,]
                {
                    { 1, "123 Street", "SomeCity", "Canada", "mymail@mail.com", "Company 1", "111-222-3333", "G4W2K0", "British Columbia" },
                    { 2, "1234 Street", "Argon", "Canada", "guy@mail.com", "Company 2", "111-225-3243", "H4M3D6", "Ontario" },
                    { 3, "12345 Street", "Borat", "Canada", "dude@mail.com", "Company 3", "999-222-3333", "J4W2R3", "Quebec" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BulkRate10", "BulkRate100", "CustomerId", "GrossWeight", "Inventory", "IsHazardous", "Name", "NetWeight", "Price", "ProductCode", "UNnumber" },
                values: new object[,]
                {
                    { 1, 4.0, 3.5, 1, 110.0, 109, true, "Prod1", 100.0, 5.0, "HMG1", "UN0072" },
                    { 2, 52.340000000000003, 49.990000000000002, 2, 13.0, 1100, false, "Prod2", 10.0, 55.700000000000003, "xx111", null },
                    { 3, 950.76999999999998, 899.95000000000005, 3, 1224.0, 55, true, "Prod3", 1090.0, 1000.5, "saf13", "UN0004" },
                    { 4, 22.399999999999999, 20.5, 1, 15.0, 54, true, "Prod4", 12.0, 25.0, "fdsfds11", "UN0004" },
                    { 5, 9.0, 8.75, 1, 22.0, 101, false, "Prod5", 15.0, 10.0, "fsdgfds123", null },
                    { 6, 49.990000000000002, 3.5, 3, 60.0, 45, false, "Prod6", 55.0, 55.0, "fdsgds1235", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CustomerId",
                table: "Products",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
