using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace RidePal.Data.Migrations
{
    public partial class Innitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7d6175c3-cba6-435f-a4f8-16a15a91892d"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("458e0d56-a7c3-4527-9500-48286e5c8e32"), new Guid("36cb2442-0a8a-407d-975a-91d243c5eafd") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("36cb2442-0a8a-407d-975a-91d243c5eafd"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("458e0d56-a7c3-4527-9500-48286e5c8e32"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("357c4045-cc68-49e3-b8ce-dfcec9a12e16"), "5f85fd60-5f2f-4e6c-8a05-43220beaeadb", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("5ed77f52-bdb4-4fd5-8bdb-36d886a99f39"), "c1aea20a-b6a0-4815-80f6-bc18905e86d6", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedOn", "Email", "EmailConfirmed", "FirstName", "Image", "IsAdmin", "IsBanned", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("dd5efef9-4450-4a7a-92de-999e3cbb7260"), 0, "21bef365-e47b-4e47-837c-4ceb0d5e2cbf", new DateTime(2020, 11, 17, 16, 12, 41, 602, DateTimeKind.Utc).AddTicks(7735), "gigenev@gmail.com", true, "Gencho", "~/images/Profile.jpg", true, false, false, "Genev", false, null, "GIGENEV@GMAIL.COM", "GIGENEV@ADMIN.COM", "AQAAAAEAACcQAAAAELTaeV2r615NbGv6Gv3/LEfqwFUDsY+s0HMWVApEyaVMjFCevBybWulaGTIcOw+pcg==", null, false, "d14bb70c-e299-4426-ae75-71819d2ce0b1", false, "gigenev@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("dd5efef9-4450-4a7a-92de-999e3cbb7260"), new Guid("357c4045-cc68-49e3-b8ce-dfcec9a12e16") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5ed77f52-bdb4-4fd5-8bdb-36d886a99f39"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("dd5efef9-4450-4a7a-92de-999e3cbb7260"), new Guid("357c4045-cc68-49e3-b8ce-dfcec9a12e16") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("357c4045-cc68-49e3-b8ce-dfcec9a12e16"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("dd5efef9-4450-4a7a-92de-999e3cbb7260"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("36cb2442-0a8a-407d-975a-91d243c5eafd"), "e7f5a2a0-a422-4c99-a497-99d251dceb47", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("7d6175c3-cba6-435f-a4f8-16a15a91892d"), "9446a206-5bad-4622-8862-ac66320a7faf", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedOn", "Email", "EmailConfirmed", "FirstName", "Image", "IsAdmin", "IsBanned", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("458e0d56-a7c3-4527-9500-48286e5c8e32"), 0, "1236e507-0103-4895-a66a-0829fc78d57a", new DateTime(2020, 11, 17, 12, 31, 33, 121, DateTimeKind.Utc).AddTicks(7058), "gigenev@gmail.com", true, "Gencho", "~/images/Profile.jpg", true, false, false, "Genev", false, null, "GIGENEV@GMAIL.COM", "GIGENEV@ADMIN.COM", "AQAAAAEAACcQAAAAEJ1cHmXU0wq3cUj0IDdqsTW2Gw6szTgA5lXbxa/FQRNC1ZmvU8qzZitQnML431m1Qg==", null, false, "2f6a76d7-aca6-48b8-bd14-bd3b749e4ed4", false, "gigenev@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("458e0d56-a7c3-4527-9500-48286e5c8e32"), new Guid("36cb2442-0a8a-407d-975a-91d243c5eafd") });
        }
    }
}
