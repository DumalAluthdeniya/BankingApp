using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingApp.Data.Migrations
{
	public partial class AddColunmEmailToBranch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "36e74614-bf2b-478e-9446-48a9c0e5e374");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Branches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "15376804-5ddc-4874-afb3-04ddbd8148b8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "83780caf-39b9-4ed5-bce4-bac92675b5bc", "dee387ae-0a82-47d2-9529-4cc0fc54a2e8", "Customer", "CUSTOMER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d7822c8b-b0bb-4501-b1d0-b7a030104c94", new DateTime(2023, 3, 13, 19, 41, 8, 993, DateTimeKind.Local).AddTicks(8883), "AQAAAAEAACcQAAAAEGVuKby5WU2iws8tWUJ4/FDquqkKoV7Qr9jHKJVcgi7tkXXSCh4WR8MzoqJF4BUxoA==", "faf8754f-5e5c-4c9c-98fa-102e76749093" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "83780caf-39b9-4ed5-bce4-bac92675b5bc");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Branches");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "0e828ceb-f200-4739-889f-ce8023f3bdb8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "36e74614-bf2b-478e-9446-48a9c0e5e374", "48aac4ed-46ea-4577-85b3-d77a1351551f", "Customer", "CUSTOMER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3251d0b3-79ce-4617-9437-c0b055f76e78", new DateTime(2023, 3, 13, 9, 23, 0, 980, DateTimeKind.Local).AddTicks(7157), "AQAAAAEAACcQAAAAEOA/TMQViYs6OmJBzWEBG1wvD89d+fbZAQOvY2qnLSUMHNl5R2wdA4Sg6W9Enevfgw==", "acd391ed-b134-4fd9-b693-db164d1bb919" });
        }
    }
}
