using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingApp.Data.Migrations
{
    public partial class AddSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "Key", "Value" },
                values: new object[] { 9, "Other_ApplicationUrl", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 9);

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
        }
    }
}
