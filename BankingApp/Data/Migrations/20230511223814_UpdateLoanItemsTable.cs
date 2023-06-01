using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingApp.Data.Migrations
{
    public partial class UpdateLoanItemsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Capital",
                table: "LoanItems",
                newName: "Principle");

            migrationBuilder.RenameColumn(
                name: "Balance",
                table: "LoanItems",
                newName: "Outstanding");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "LoanItems",
                newName: "EndingBalance");

            migrationBuilder.AddColumn<decimal>(
                name: "BeginningBalance",
                table: "LoanItems",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "084ca484-9316-4751-bac5-ceced910c556");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd0ff8e2-f1a8-4a42-835e-d4aaff7e7b8d",
                column: "ConcurrencyStamp",
                value: "1d2d9590-d2f9-4e36-80da-59a9029130bf");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c408d49b-879d-44dd-8193-38f004250403", new DateTime(2023, 5, 12, 8, 38, 13, 295, DateTimeKind.Local).AddTicks(1051), "AQAAAAEAACcQAAAAEK8wwJULX/bEQcLrKkoTrzEjrN7AkipHIZs7ih+vLtEH5SNMeI1xDasw5MTUQtVJBQ==", "9b70b4a6-498d-4d3f-8119-909cbde762ee" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BeginningBalance",
                table: "LoanItems");

            migrationBuilder.RenameColumn(
                name: "Principle",
                table: "LoanItems",
                newName: "Capital");

            migrationBuilder.RenameColumn(
                name: "Outstanding",
                table: "LoanItems",
                newName: "Balance");

            migrationBuilder.RenameColumn(
                name: "EndingBalance",
                table: "LoanItems",
                newName: "Amount");

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
    }
}
