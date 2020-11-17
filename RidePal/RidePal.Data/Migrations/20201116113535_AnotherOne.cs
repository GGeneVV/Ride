using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace RidePal.Data.Migrations
{
    public partial class AnotherOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e831dbf3-40ce-4c8d-b6d4-3e200b97ad57"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("f77128e3-1216-4fe6-80c6-4ae67a5549b7"), new Guid("f5097e03-0470-4339-8360-21fcfb9a414f") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f5097e03-0470-4339-8360-21fcfb9a414f"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f77128e3-1216-4fe6-80c6-4ae67a5549b7"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("77cdcd34-15ce-4b22-a9eb-ac27d3a2cc4b"), "9d97ca4c-58f8-4da5-adf4-3a46e7c9e8cc", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("53ac1130-75c7-4b39-93c5-b6dd2ac30da4"), "5e13dbd9-b7c9-43a5-bc64-0c4eee7baae3", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedOn", "Email", "EmailConfirmed", "FirstName", "Image", "IsAdmin", "IsBanned", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("55075023-3090-4113-8eac-0144fe314c64"), 0, "ea4cefd5-2bc6-4f92-854e-0d210dd0d5e6", new DateTime(2020, 11, 16, 11, 35, 34, 790, DateTimeKind.Utc).AddTicks(8465), "gigenev@gmail.com", true, "Gencho", "~/images/Profile.jpg", true, false, false, "Genev", false, null, "GIGENEV@GMAIL.COM", "GIGENEV@ADMIN.COM", "AQAAAAEAACcQAAAAEODv2sTSlYa2B1abi82m3klRXyHAu3EWxjtmacp+aPGL0DZoL0qFQ9A2te3+zi4Txg==", null, false, "ee29cb57-d69f-4de6-ad27-ae83043ada12", false, "gigenev@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("55075023-3090-4113-8eac-0144fe314c64"), new Guid("77cdcd34-15ce-4b22-a9eb-ac27d3a2cc4b") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("53ac1130-75c7-4b39-93c5-b6dd2ac30da4"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("55075023-3090-4113-8eac-0144fe314c64"), new Guid("77cdcd34-15ce-4b22-a9eb-ac27d3a2cc4b") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("77cdcd34-15ce-4b22-a9eb-ac27d3a2cc4b"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("55075023-3090-4113-8eac-0144fe314c64"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("f5097e03-0470-4339-8360-21fcfb9a414f"), "4a13416e-165f-4831-aa06-491037c6bb08", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("e831dbf3-40ce-4c8d-b6d4-3e200b97ad57"), "e337563d-f6c5-4f96-a229-50d4d58fc531", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedOn", "Email", "EmailConfirmed", "FirstName", "Image", "IsAdmin", "IsBanned", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("f77128e3-1216-4fe6-80c6-4ae67a5549b7"), 0, "61df8245-14a5-44f6-b042-702830d0c6c6", new DateTime(2020, 11, 15, 14, 28, 43, 995, DateTimeKind.Utc).AddTicks(9629), "gigenev@gmail.com", true, "Gencho", "~/images/Profile.jpg", true, false, false, "Genev", false, null, "GIGENEV@GMAIL.COM", "GIGENEV@ADMIN.COM", "AQAAAAEAACcQAAAAEFj0R0Si78NbWUFnlekihmbFBkjWtHzIA2A3BiScLGo8TBvxebeOew6n/WraGgC8/Q==", null, false, "0aefe0e5-4fae-474f-b817-46d2b721fc1b", false, "gigenev@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("f77128e3-1216-4fe6-80c6-4ae67a5549b7"), new Guid("f5097e03-0470-4339-8360-21fcfb9a414f") });
        }
    }
}
