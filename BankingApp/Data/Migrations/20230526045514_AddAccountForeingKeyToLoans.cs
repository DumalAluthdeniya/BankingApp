using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingApp.Data.Migrations
{
    public partial class AddAccountForeingKeyToLoans : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountNo",
                table: "Loans");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Loans",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "9501b1de-b3d0-41d5-a5ab-9a7fe1f1e4a8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd0ff8e2-f1a8-4a42-835e-d4aaff7e7b8d",
                column: "ConcurrencyStamp",
                value: "b12045a7-5d46-4834-931e-2c402e3197e8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a832a2e7-2292-4875-bfd5-627ed9cabec3", new DateTime(2023, 5, 26, 14, 55, 13, 735, DateTimeKind.Local).AddTicks(8172), "AQAAAAEAACcQAAAAEP1DNZq+33bd0rtjx6P6tc+BXHsM7GNy2eoW4oo67cmTmaSQNTu7XDj8IFNIPUNugA==", "079ecd52-37b3-4ee0-bddb-5cf2ff0dd3ae" });

            migrationBuilder.CreateIndex(
                name: "IX_Loans_AccountId",
                table: "Loans",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Accounts_AccountId",
                table: "Loans",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Accounts_AccountId",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Loans_AccountId",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Loans");

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
                value: "6c5f3ee5-bc50-423e-b5de-c1fbb5177b5b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd0ff8e2-f1a8-4a42-835e-d4aaff7e7b8d",
                column: "ConcurrencyStamp",
                value: "ca1dde81-7980-421b-856e-cf7e8f99dcf1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "885d43be-3a70-4161-9c87-a11169c7b045", new DateTime(2023, 5, 25, 22, 6, 43, 667, DateTimeKind.Local).AddTicks(9125), "AQAAAAEAACcQAAAAEAL9ZN7Bmw5WVaUjkHNOaPEG5xYnm2GyHVGWel/hObOEBYMPLQ9UW+rIMyxK/nUlgw==", "ac1e64b4-42a4-4c3d-af62-8baf1becf9e2" });
        }
    }
}
