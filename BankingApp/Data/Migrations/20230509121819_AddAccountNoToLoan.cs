using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingApp.Data.Migrations
{
    public partial class AddAccountNoToLoan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AccountNo",
                table: "Loans",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "8d955ab5-b047-4f91-be98-2d9dbd0ad51b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd0ff8e2-f1a8-4a42-835e-d4aaff7e7b8d",
                column: "ConcurrencyStamp",
                value: "d15f444e-2e12-4b47-b5d4-60c47bafa907");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "40d608f1-951a-419c-9a33-55ec00830be2", new DateTime(2023, 5, 9, 22, 18, 18, 27, DateTimeKind.Local).AddTicks(7638), "AQAAAAEAACcQAAAAEO8B05af5WHE2WodogX3oyFYrEIn82yhaIUM/gE3Gpig/wOJ/FHXGVj2MGmcMfLwTg==", "175cb415-908e-4b13-8c23-7d83b562a09f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountNo",
                table: "Loans");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "8ede4dd1-8eb8-46da-9150-4943eb0ff992");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd0ff8e2-f1a8-4a42-835e-d4aaff7e7b8d",
                column: "ConcurrencyStamp",
                value: "648dabb6-1050-4d61-8dfe-a9f8ea7780f6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9569c18e-4d6e-47d3-bb80-c99a622ddabd", new DateTime(2023, 5, 6, 13, 42, 26, 555, DateTimeKind.Local).AddTicks(9300), "AQAAAAEAACcQAAAAEEkpPW3d9/bmEkGs3EIZANw/Nz3Pz3zCncR82UdiEde7miL7sUp4SGy+6oI3trz4ew==", "63c4617e-73f1-4c96-8fdd-3b48f7f378fb" });
        }
    }
}
