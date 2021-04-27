using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.Infrastructure.Migrations
{
    public partial class add_default_admin_role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "f68ef0f8-1dfe-401b-89b6-8298fd827299", "d2b75e32-cdea-4148-90b9-98e55e23c768", "Admin", "ADMIN" },
                    { "3a93386e-133e-40da-aed7-bd2efd793fa8", "fbb1a294-8a76-459f-810e-39deea9b0379", "Moderator", "MODERATOR" },
                    { "c5f7682f-3598-4a33-98b6-2a3fa7d89ead", "da73b1ef-daa2-4bad-b7e4-aecbb043aedf", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ActivatedAt", "Address", "City", "ConcurrencyStamp", "Country", "CreatedAt", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "IdentificationNumber", "IsActive", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PersonalNumber", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3f34424b-40c8-435b-85ea-064ac4534c9b", 0, null, null, null, "53b0cc50-961d-4555-9ce8-67c0f26c3284", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "admin@gmail.com", true, "Levan", null, true, "Doliashvili", false, null, "ADMIN@GMAIL.COM", null, "AKrQqC4zxZzNrvdHWfPLR4GKXXt9GvnNZIFZrtaPqzL5VS3VzD+QnT09iAEW1eakLQ==", null, null, true, null, "029d1a40-57c5-48d8-99c3-38d0cfeff4b4", false, "admin" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "f68ef0f8-1dfe-401b-89b6-8298fd827299", "3f34424b-40c8-435b-85ea-064ac4534c9b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "3a93386e-133e-40da-aed7-bd2efd793fa8");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "c5f7682f-3598-4a33-98b6-2a3fa7d89ead");

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f68ef0f8-1dfe-401b-89b6-8298fd827299", "3f34424b-40c8-435b-85ea-064ac4534c9b" });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "f68ef0f8-1dfe-401b-89b6-8298fd827299");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "3f34424b-40c8-435b-85ea-064ac4534c9b");
        }
    }
}
