using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RidePal.Data.Migrations
{
    public partial class anotherOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { new Guid("f2d72b4d-ab14-43fa-8ddf-1877336449db"), "c6f372d2-d8fd-4da5-9c63-e24ccd9e78f8", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("dcbc310f-80cb-4af5-8c93-2b5bfe5a9c87"), "ec1bbed3-876e-44f9-ac80-a5e2459eae6f", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedOn", "Email", "EmailConfirmed", "FirstName", "Image", "IsAdmin", "IsBanned", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("cabb9b5b-85f9-42b6-98f0-511b4e688903"), 0, "90053ff0-be80-4546-bb84-a875b37fccd0", new DateTime(2020, 11, 18, 18, 30, 37, 19, DateTimeKind.Utc).AddTicks(8420), "gigenev@gmail.com", true, "Gencho", "~/images/Profile.jpg", true, false, false, "Genev", false, null, "GIGENEV@GMAIL.COM", "GIGENEV@ADMIN.COM", "AQAAAAEAACcQAAAAEN7dvJXkNzdm0l4kn/74P6cX0DqCdtf+BR6DQ3bWeBwsZ0IffdFZGolOx6ZNrZS1kg==", null, false, "df4a9d3c-9539-44a2-be7c-8ca6472e0cd8", false, "gigenev@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("cabb9b5b-85f9-42b6-98f0-511b4e688903"), new Guid("f2d72b4d-ab14-43fa-8ddf-1877336449db") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dcbc310f-80cb-4af5-8c93-2b5bfe5a9c87"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("cabb9b5b-85f9-42b6-98f0-511b4e688903"), new Guid("f2d72b4d-ab14-43fa-8ddf-1877336449db") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f2d72b4d-ab14-43fa-8ddf-1877336449db"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cabb9b5b-85f9-42b6-98f0-511b4e688903"));

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
    }
}
