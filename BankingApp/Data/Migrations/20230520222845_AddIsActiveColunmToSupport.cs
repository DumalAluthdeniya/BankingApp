using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingApp.Data.Migrations
{
    public partial class AddIsActiveColunmToSupport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Supports",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "051fc8be-2537-48c8-9ec4-809f1e756f96");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd0ff8e2-f1a8-4a42-835e-d4aaff7e7b8d",
                column: "ConcurrencyStamp",
                value: "3a4b9a72-9141-4b33-a959-ec5f2ef73673");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "806d6a1e-a61d-41bb-8d21-bbd2069a3703", new DateTime(2023, 5, 21, 8, 28, 44, 379, DateTimeKind.Local).AddTicks(5906), "AQAAAAEAACcQAAAAEKgbBsV2wtf6ebZq2VME1g5wGYYnM1vc9UgJEOHGIZVFgRZTJiyQTN7kBASPF9stoQ==", "e4db5c4c-7089-4a35-a152-756c534d8f1e" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Supports");

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
    }
}
