using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RidePal.Data.Migrations
{
    public partial class LastOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15751626-c238-479b-9628-b8b503b587c1"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("dbaf3f8c-f7b9-4a43-a377-cefd0697dba6"), new Guid("c8c26c3e-28d1-4933-9f53-531973020bcb") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c8c26c3e-28d1-4933-9f53-531973020bcb"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("dbaf3f8c-f7b9-4a43-a377-cefd0697dba6"));

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "AspNetUsers",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { new Guid("c8c26c3e-28d1-4933-9f53-531973020bcb"), "b29ed945-acf4-459b-8d6b-458283463a36", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("15751626-c238-479b-9628-b8b503b587c1"), "fa4c4741-98fb-4679-a5cc-c766e1ca1d21", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedOn", "Email", "EmailConfirmed", "FirstName", "Image", "IsAdmin", "IsBanned", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("dbaf3f8c-f7b9-4a43-a377-cefd0697dba6"), 0, "cbda5bad-35a6-4c9f-993f-b8355ad6d6d4", new DateTime(2020, 11, 9, 9, 49, 50, 775, DateTimeKind.Utc).AddTicks(8939), "gigenev@gmail.com", true, "Gencho", "~/images/Profile.jpg", true, false, false, "Genev", false, null, "GIGENEV@GMAIL.COM", "GIGENEV@ADMIN.COM", "AQAAAAEAACcQAAAAEMR10PbLQ2Hltgj0Vc92eMERlePpiFgNlUq6yvLyfQQ0xNYos4n0ADx9RXOorqVKlA==", null, false, "7cd48c9a-893d-40f6-9369-a7071268c35a", false, "gigenev@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("dbaf3f8c-f7b9-4a43-a377-cefd0697dba6"), new Guid("c8c26c3e-28d1-4933-9f53-531973020bcb") });
        }
    }
}
