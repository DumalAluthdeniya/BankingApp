using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingApp.Data.Migrations
{
	public partial class AddLoanItemsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoanItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanId = table.Column<int>(type: "int", nullable: true),
                    Capital = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Interest = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanItem_Loans_LoanId",
                        column: x => x.LoanId,
                        principalTable: "Loans",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "5305ec13-ab86-49cb-b298-4a454570575d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd0ff8e2-f1a8-4a42-835e-d4aaff7e7b8d",
                column: "ConcurrencyStamp",
                value: "d76afa54-e2ad-47bd-a8e6-01faf30c5ceb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "148f4c3d-f98e-440e-b4b3-a3addb378910", new DateTime(2023, 3, 19, 14, 50, 45, 406, DateTimeKind.Local).AddTicks(1754), "AQAAAAEAACcQAAAAECyeniZaqzsWWE7Vbdkx/4glYDnP6wFLgoW22BfHmxRrNkEPvJJWDng4SoCfEK0S1Q==", "1ee6eeba-89e2-4097-8c3d-67dc41775f4e" });

            migrationBuilder.CreateIndex(
                name: "IX_LoanItem_LoanId",
                table: "LoanItem",
                column: "LoanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanItem");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "0c17abfd-a7ae-4cbc-be9c-0ca671c71d49");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd0ff8e2-f1a8-4a42-835e-d4aaff7e7b8d",
                column: "ConcurrencyStamp",
                value: "8dcbca5d-9c59-43fb-aecf-7f54c74e5935");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "720a936e-8d8d-40c2-a9ba-9fc0efe4214a", new DateTime(2023, 3, 16, 23, 30, 19, 578, DateTimeKind.Local).AddTicks(4108), "AQAAAAEAACcQAAAAEFthDv1xAIwunHWEk+dSqj2zmHxrIt2Au03GOqhEbM7EAGJXtPiz0AQNG0XFkM+Sew==", "b4aebdd4-ddc0-4e60-894f-7031a9204016" });
        }
    }
}
