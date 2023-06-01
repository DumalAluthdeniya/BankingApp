using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingApp.Data.Migrations
{
	public partial class ColunmUpdatedInExchgeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rate",
                table: "Exchanges",
                newName: "SellingRate");

            migrationBuilder.AddColumn<decimal>(
                name: "BuyingRate",
                table: "Exchanges",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MarketRatePreviousDay",
                table: "Exchanges",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Symbol",
                table: "Exchanges",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "21b04376-7dbb-4bec-beda-b12189490e65");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd0ff8e2-f1a8-4a42-835e-d4aaff7e7b8d",
                column: "ConcurrencyStamp",
                value: "cc2192f0-9e9b-4042-b1ea-5b18765987b0");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1fd10ba0-31b3-4503-bd62-e43ea96112d6", new DateTime(2023, 3, 19, 16, 21, 28, 856, DateTimeKind.Local).AddTicks(2137), "AQAAAAEAACcQAAAAEOn3TcwyLEdc67wfpXvXceBL7wTBKScg0f0LOMFmI9nXKVOJ8jZJRMBQTAWg3lthsA==", "e5c7d53d-8689-4bb1-a52a-5240cae7cacb" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyingRate",
                table: "Exchanges");

            migrationBuilder.DropColumn(
                name: "MarketRatePreviousDay",
                table: "Exchanges");

            migrationBuilder.DropColumn(
                name: "Symbol",
                table: "Exchanges");

            migrationBuilder.RenameColumn(
                name: "SellingRate",
                table: "Exchanges",
                newName: "Rate");

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
        }
    }
}
