using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.Infrastructure.Migrations
{
    public partial class EmailAndUserNameIsRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "3a93386e-133e-40da-aed7-bd2efd793fa8",
                column: "ConcurrencyStamp",
                value: "4a133119-7d5d-4c25-a329-d8ec804fdc8d");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "c5f7682f-3598-4a33-98b6-2a3fa7d89ead",
                column: "ConcurrencyStamp",
                value: "0d5d8cd9-36a8-4639-aad4-26d09ceff8b3");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "f68ef0f8-1dfe-401b-89b6-8298fd827299",
                column: "ConcurrencyStamp",
                value: "f1d7385d-ea3e-49ee-8bf5-6505fddeb315");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "3f34424b-40c8-435b-85ea-064ac4534c9b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "82782359-ec74-40a1-a488-37ac071f1b41", "AQAAAAEAACcQAAAAEGMuxrlH1BkfFNaIKtrb0EPmMc4SMPeJTlArtgEuTn+31pX/bUjbNKngprhqK5zRWA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "3a93386e-133e-40da-aed7-bd2efd793fa8",
                column: "ConcurrencyStamp",
                value: "f85a5357-a5f8-4443-97d8-fa8055693f59");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "c5f7682f-3598-4a33-98b6-2a3fa7d89ead",
                column: "ConcurrencyStamp",
                value: "aff948c5-074a-49a2-8902-317a3ce7538a");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "f68ef0f8-1dfe-401b-89b6-8298fd827299",
                column: "ConcurrencyStamp",
                value: "8c3984c7-e14d-449a-80ab-a5a8e0b7d12c");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "3f34424b-40c8-435b-85ea-064ac4534c9b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "519cb191-12fb-447f-a6cb-88ae2fd4ec19", "AQAAAAEAACcQAAAAEAC3SblNGiDR22mb0ufgCFpi2FhOPyPfODDesPhObpdD2z5c/4H3vdDTQwqChWkfWg==" });
        }
    }
}
