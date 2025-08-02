using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StockPlaform.Migrations
{
    /// <inheritdoc />
    public partial class AddAppUserRelationToComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "32459823-bfae-470f-ab38-2bd5098b0bbc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d749b546-538f-4e24-ad5e-b265a12e2d7b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f6442525-550e-46f4-8f35-546681375e01");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "14f0b495-2c51-4b6f-8a44-5d6c035863fa", null, "User", "USER" },
                    { "6579ec04-c8b9-4ba5-b73c-f48f5583797a", null, "Manager", "MANAGER" },
                    { "efaf8334-e27d-4709-9a8d-4e40c340ebb1", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14f0b495-2c51-4b6f-8a44-5d6c035863fa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6579ec04-c8b9-4ba5-b73c-f48f5583797a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "efaf8334-e27d-4709-9a8d-4e40c340ebb1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "32459823-bfae-470f-ab38-2bd5098b0bbc", null, "Manager", "MANAGER" },
                    { "d749b546-538f-4e24-ad5e-b265a12e2d7b", null, "Admin", "ADMIN" },
                    { "f6442525-550e-46f4-8f35-546681375e01", null, "User", "USER" }
                });
        }
    }
}
