using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingApp.Data.Migrations
{
	public partial class AddCollataralColunmToLoans : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Collateral",
                table: "Loans",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "0a8b080a-9d9a-4aa3-9b77-2c0117b408a9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd0ff8e2-f1a8-4a42-835e-d4aaff7e7b8d",
                column: "ConcurrencyStamp",
                value: "c0b24cc0-38fd-46be-aa6e-2c67050a90d3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3f912bd3-bb30-497e-975a-c88a1ad5a2e7", new DateTime(2023, 4, 3, 14, 31, 13, 455, DateTimeKind.Local).AddTicks(2649), "AQAAAAEAACcQAAAAEFsQUkP5M/Ft29M1+QBfWYWg05UdcYXN6SnTWBezEazHG7REXlIXq36OGKeUaEogHg==", "9987e2ba-4827-4d93-aadb-c1b8afb9abc7" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Collateral",
                table: "Loans");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "d515aeb2-bdbf-4956-bc75-00ff18f18fc4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd0ff8e2-f1a8-4a42-835e-d4aaff7e7b8d",
                column: "ConcurrencyStamp",
                value: "fa9e88b0-3d12-488e-a839-8f000afcdab5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fecc8941-4253-4d59-b241-85ffdfb2b6e4", new DateTime(2023, 4, 2, 15, 6, 13, 751, DateTimeKind.Local).AddTicks(5176), "AQAAAAEAACcQAAAAEGr7FCTG2bRIcffGuQcgeqdNQBdaIKd1lzayV4cfwr9RNUmKdLG8BBydoXPpcr99sw==", "a8f02516-f877-4eae-a5c9-b2fe888bae1d" });
        }
    }
}
