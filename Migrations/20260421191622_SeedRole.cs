using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class SeedRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0a2abbbc-0189-4576-88a7-5600f47c868d", "bfcbfef8-c8be-4a85-8126-b7ab51b94785", "User", "USER" },
                    { "9615c412-70ff-4340-8746-3a7469fbee6d", "3fdfb2cc-bf3b-4c62-aea3-2a48c1294bfd", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0a2abbbc-0189-4576-88a7-5600f47c868d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9615c412-70ff-4340-8746-3a7469fbee6d");
        }
    }
}
