using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Capstone.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialProductModelAndDatabaseCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    Inventory = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "GrossWeight", "Inventory", "IsHazardous", "Name", "NetWeight", "ProductCode" },
                values: new object[,]
                {
                    { 1, 110.0, 10, true, "Prod1", 100.0, "HMG1" },
                    { 2, 13.0, 11, false, "Prod2", 10.0, "xx111" },
                    { 3, 1224.0, 1, true, "Prod3", 1090.0, "saf13" },
                    { 4, 15.0, 54, true, "Prod4", 12.0, "fdsfds11" },
                    { 5, 22.0, 101, false, "Prod5", 15.0, "fsdgfds123" },
                    { 6, 60.0, 45, false, "Prod6", 55.0, "fdsgds1235" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
