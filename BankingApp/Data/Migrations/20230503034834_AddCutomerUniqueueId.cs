using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingApp.Data.Migrations
{
    public partial class AddCutomerUniqueueId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CustomerUniqueId",
                table: "AspNetUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "b5822b6f-7615-4c32-b5f7-6666a5d843d7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd0ff8e2-f1a8-4a42-835e-d4aaff7e7b8d",
                column: "ConcurrencyStamp",
                value: "5d4c0b9e-567c-4e11-9e77-11b060374565");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4624bb7a-4073-4cb0-9d10-4cd368be5ce1", new DateTime(2023, 5, 3, 13, 48, 33, 925, DateTimeKind.Local).AddTicks(4608), "AQAAAAEAACcQAAAAEIwzSkR1KHFY3D4Dkyl0ILRSVv+sNID7NetEM0zsiFgjztkJM2ohFCjr4JOcsljJBQ==", "29c07dd6-d909-4e47-baaf-07067b3a9406" });

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "Key", "Value" },
                values: new object[] { 8, "Other_BaseCustomerId", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DropColumn(
                name: "CustomerUniqueId",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "42097813-c03c-4609-a10b-0f3d7d2e9ea2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd0ff8e2-f1a8-4a42-835e-d4aaff7e7b8d",
                column: "ConcurrencyStamp",
                value: "3ce5bf0a-ce85-4aca-81eb-bf806b6cd30e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "50095fe3-7ac4-40ea-a4a1-234fba3eceaf", new DateTime(2023, 4, 27, 12, 37, 14, 471, DateTimeKind.Local).AddTicks(1271), "AQAAAAEAACcQAAAAEM4NFiH+9yNfdQm1BCHIjt74zPREz3XfeFsUpKFlU1fcnqst7D+HCLwQWQPSRYX5SA==", "f992c6ba-d194-4f49-9994-b7437de73ac0" });
        }
    }
}
