using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingApp.Data.Migrations
{
    public partial class AddColunmSquenceNoToPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SequenceNo",
                table: "Permissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "adbd84af-ff35-4b0f-b3ea-f6bb4ed9649b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd0ff8e2-f1a8-4a42-835e-d4aaff7e7b8d",
                column: "ConcurrencyStamp",
                value: "98877345-592f-48d5-9747-52cd48c26d7e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "26276d53-4edb-4209-86cc-5e41810bcb5b", new DateTime(2023, 5, 24, 21, 33, 28, 347, DateTimeKind.Local).AddTicks(6963), "AQAAAAEAACcQAAAAECgVX4tnJCUTYoSSlCDH3YNkvp6lnJQqefIxRpfe3HY2C1R4/tYZbXfSV5T2dKcw4w==", "526babdb-bdcb-41c5-b2d8-a1c5dcba1bc0" });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "SequenceNo",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "SequenceNo",
                value: 8);

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "SequenceNo",
                value: 9);

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "SequenceNo",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "SequenceNo",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "SequenceNo",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "SequenceNo",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "SequenceNo",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "SequenceNo",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "SequenceNo",
                value: 10);

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "SequenceNo",
                value: 11);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SequenceNo",
                table: "Permissions");

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
    }
}
