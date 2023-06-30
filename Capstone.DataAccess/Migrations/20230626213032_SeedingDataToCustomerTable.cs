using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Capstone.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataToCustomerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "City", "Country", "Email", "Name", "Phone", "PostalCode", "Region" },
                values: new object[,]
                {
                    { 1, "123 Street", "SomeCity", "United States", "mymail@mail.com", "Company 1", "111-222-3333", "12345", "Washington Stats" },
                    { 2, "1234 Street", "Argon", "United States", "guy@mail.com", "Company 2", "111-225-3243", "45678", "New York" },
                    { 3, "12345 Street", "Borat", "Canada", "dude@mail.com", "Company 3", "999-222-3333", "J4W2R3", "Quebec" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
