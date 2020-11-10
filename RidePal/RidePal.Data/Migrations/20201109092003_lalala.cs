using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace RidePal.Data.Migrations
{
    public partial class lalala : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("51446abf-cde4-4569-885c-0932e45d2adc"), "09737377-72d7-458c-a2c1-8a2a4eaae011", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("c9c2dbd4-4c9d-42c5-a8c3-f6b1611901c7"), "86daaccb-8104-4862-b74e-2ffaf89bde47", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("51446abf-cde4-4569-885c-0932e45d2adc"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c9c2dbd4-4c9d-42c5-a8c3-f6b1611901c7"));
        }
    }
}
