using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingApp.Data.Migrations
{
    public partial class DbChnages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "a048d589-31fd-48f4-a674-ee7f3bf4b419");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd0ff8e2-f1a8-4a42-835e-d4aaff7e7b8d",
                column: "ConcurrencyStamp",
                value: "01af0432-5873-4d89-a1bc-1b3b591a6b32");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bddbecc8-5de7-4e02-9a59-4b778baa7474", new DateTime(2023, 3, 13, 23, 36, 23, 835, DateTimeKind.Local).AddTicks(2352), "AQAAAAEAACcQAAAAEJFJ6eY13EdFrWWwoXlw7qoR+c2BZXco3+Lk2QD65rLpeRZ4xz/l+xbK9VKjLCvFYA==", "6a33ea19-2c7e-4289-b5fa-db72b78e99c3" });
        }
    }
}
