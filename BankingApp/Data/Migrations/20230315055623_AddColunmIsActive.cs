using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingApp.Data.Migrations
{
    public partial class AddColunmIsActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "EmployeePermissions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "59cccd85-f883-4611-a980-6b561c348286");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd0ff8e2-f1a8-4a42-835e-d4aaff7e7b8d",
                column: "ConcurrencyStamp",
                value: "3ba90775-c072-4620-a4b6-ecefa127d24f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "80edf6f1-486e-4c81-8d5a-439a90922dd6", new DateTime(2023, 3, 15, 16, 56, 21, 401, DateTimeKind.Local).AddTicks(856), "AQAAAAEAACcQAAAAEDaP07FiTxdaB0SBv/03Kun8S7QTS6/soyGka1ri+IxnUi4AGPJnfF5/zqA1+T+1BA==", "895643a2-5a79-409b-92cd-b6499c1a1663" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "EmployeePermissions");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "74be040e-e07b-4d3b-8a93-f4e39b80103e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd0ff8e2-f1a8-4a42-835e-d4aaff7e7b8d",
                column: "ConcurrencyStamp",
                value: "c20a9f5e-1ce4-45a2-b8bd-38f119b5fd6e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ad4aca4e-829d-4df8-8c86-fb2f18638863", new DateTime(2023, 3, 13, 23, 56, 49, 159, DateTimeKind.Local).AddTicks(9250), "AQAAAAEAACcQAAAAEEUqaKgM1t7go24rF8Ehq9l3mS1kSm69BuyL8KUUVSXqxrnWsfktis1/nIdOoyItMQ==", "eee2d2e2-1f80-47c5-82bc-442b635f4954" });
        }
    }
}
