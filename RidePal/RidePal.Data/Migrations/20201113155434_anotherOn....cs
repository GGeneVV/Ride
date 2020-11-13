using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RidePal.Data.Migrations
{
    public partial class anotherOn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a412873f-bbb1-4b16-b012-a38c9109f9f4"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("227dd999-2b40-4bd1-a87e-3a4b4faec823"), new Guid("e8336579-5066-4a78-97f9-75d7d333cd78") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e8336579-5066-4a78-97f9-75d7d333cd78"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("227dd999-2b40-4bd1-a87e-3a4b4faec823"));

            migrationBuilder.DropColumn(
                name: "Password",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("ceb2b58e-b8d1-47aa-99e0-80d918c2e741"), "f6c774d7-3067-4371-99ce-39dfd75d756b", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("ad95bfc0-9ab5-453d-abcb-c00f71b4e4fa"), "dfbc8a5a-e7e9-4e53-971b-1ccba0d0284a", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedOn", "Email", "EmailConfirmed", "FirstName", "Image", "IsAdmin", "IsBanned", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("42ce8eaa-5ad8-4e9d-bf91-610fb8254fdf"), 0, "8901b516-98fe-4413-bbcd-2c771a2cef1b", new DateTime(2020, 11, 13, 15, 54, 33, 251, DateTimeKind.Utc).AddTicks(8191), "gigenev@gmail.com", true, "Gencho", "~/images/Profile.jpg", true, false, false, "Genev", false, null, "GIGENEV@GMAIL.COM", "GIGENEV@ADMIN.COM", "AQAAAAEAACcQAAAAELvCRu/x/kVoj22HMtfpWZ41oVSXJGqU2TKJx4Ws0ov/PN1+dX2A6nFSzCCpR5rs6A==", null, false, "65e262d0-08d7-4211-8b84-7f701b37fe7f", false, "gigenev@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("42ce8eaa-5ad8-4e9d-bf91-610fb8254fdf"), new Guid("ceb2b58e-b8d1-47aa-99e0-80d918c2e741") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ad95bfc0-9ab5-453d-abcb-c00f71b4e4fa"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("42ce8eaa-5ad8-4e9d-bf91-610fb8254fdf"), new Guid("ceb2b58e-b8d1-47aa-99e0-80d918c2e741") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ceb2b58e-b8d1-47aa-99e0-80d918c2e741"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("42ce8eaa-5ad8-4e9d-bf91-610fb8254fdf"));

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("e8336579-5066-4a78-97f9-75d7d333cd78"), "147cb269-06dc-41bc-b23d-90415e6fa62f", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("a412873f-bbb1-4b16-b012-a38c9109f9f4"), "c65c0b35-9b6a-4918-bf0b-5b621996d8b4", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedOn", "Email", "EmailConfirmed", "FirstName", "Image", "IsAdmin", "IsBanned", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Token", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("227dd999-2b40-4bd1-a87e-3a4b4faec823"), 0, "dc8192b8-1ff4-4903-b487-80169c2b3ac2", new DateTime(2020, 11, 12, 22, 37, 31, 277, DateTimeKind.Utc).AddTicks(8636), "gigenev@gmail.com", true, "Gencho", "~/images/Profile.jpg", true, false, false, "Genev", false, null, "GIGENEV@GMAIL.COM", "GIGENEV@ADMIN.COM", null, "AQAAAAEAACcQAAAAECHgjfFBX8eJCd838DIuNFdIkRp8vLDgqLiNiEgY9etGrv4lv5pZ3DkK457QDYoIDg==", null, false, "26f20da8-a69d-4c8e-8744-5170b9d4e06a", null, false, "gigenev@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("227dd999-2b40-4bd1-a87e-3a4b4faec823"), new Guid("e8336579-5066-4a78-97f9-75d7d333cd78") });
        }
    }
}
