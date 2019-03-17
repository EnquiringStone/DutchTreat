using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DutchTreat.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "OrderDate", "OrderNumber" },
                values: new object[] { new Guid("4313ebfd-6948-4759-ad11-5e40337a7f94"), new DateTime(2019, 3, 17, 21, 45, 33, 924, DateTimeKind.Local), "12345" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("4313ebfd-6948-4759-ad11-5e40337a7f94"));
        }
    }
}
