using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingApp.Data.Migrations
{
	public partial class UpdateLoanItemsTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanItem_Loans_LoanId",
                table: "LoanItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoanItem",
                table: "LoanItem");

            migrationBuilder.RenameTable(
                name: "LoanItem",
                newName: "LoanItems");

            migrationBuilder.RenameIndex(
                name: "IX_LoanItem_LoanId",
                table: "LoanItems",
                newName: "IX_LoanItems_LoanId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoanItems",
                table: "LoanItems",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "6d3ff039-9363-442f-b050-43dde22637f2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd0ff8e2-f1a8-4a42-835e-d4aaff7e7b8d",
                column: "ConcurrencyStamp",
                value: "2fb23c99-0964-419b-98c6-c6c7d2decff4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ec6b2be7-40a1-403b-be9a-a7a8ca7f5ebc", new DateTime(2023, 4, 3, 22, 6, 27, 837, DateTimeKind.Local).AddTicks(4815), "AQAAAAEAACcQAAAAEADynEizY277j8XNhDipYl6Ue8mmOPVQWMYBrMmnmsjYp9/hNSvfqP/yD140G/N5rA==", "505e5ef8-11c6-4dab-9e68-41fadd97a443" });

            migrationBuilder.AddForeignKey(
                name: "FK_LoanItems_Loans_LoanId",
                table: "LoanItems",
                column: "LoanId",
                principalTable: "Loans",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanItems_Loans_LoanId",
                table: "LoanItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoanItems",
                table: "LoanItems");

            migrationBuilder.RenameTable(
                name: "LoanItems",
                newName: "LoanItem");

            migrationBuilder.RenameIndex(
                name: "IX_LoanItems_LoanId",
                table: "LoanItem",
                newName: "IX_LoanItem_LoanId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoanItem",
                table: "LoanItem",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_LoanItem_Loans_LoanId",
                table: "LoanItem",
                column: "LoanId",
                principalTable: "Loans",
                principalColumn: "Id");
        }
    }
}
