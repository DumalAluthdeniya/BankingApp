using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingApp.Data.Migrations
{
	public partial class AddDescColunmToTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reference",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "a68b1276-a3e5-488f-be61-69c0e82f6882");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd0ff8e2-f1a8-4a42-835e-d4aaff7e7b8d",
                column: "ConcurrencyStamp",
                value: "f52fcecd-0884-4f98-8a2c-855dd0703246");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c38b7c22-654b-4f1e-84ba-20b75cddca5c", new DateTime(2023, 3, 22, 20, 23, 38, 273, DateTimeKind.Local).AddTicks(611), "AQAAAAEAACcQAAAAENeLEm/Ty7v752WwM/dC0MJEG6ue78TS3vJi+yjEMoiWz3t+QJUOcWZIIvPOgyQ82Q==", "a78715e7-5d23-46ed-894e-6fae1683f048" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Reference",
                table: "Transactions");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "f8c01a10-dc5c-4e84-9318-7961e773d653");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd0ff8e2-f1a8-4a42-835e-d4aaff7e7b8d",
                column: "ConcurrencyStamp",
                value: "1ed007b4-39b2-4542-9b40-95394f248a86");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f5a97864-e3b5-4886-ae03-35136373c1a8", new DateTime(2023, 3, 21, 22, 6, 42, 167, DateTimeKind.Local).AddTicks(5487), "AQAAAAEAACcQAAAAEMyWJod1hwLhtbPRf4Y8uEd2Wbby+JJNAWckVhzDeLOaQcPVNfbjRoMc3NPa7gYjZg==", "6c6e9a25-ac63-418d-a925-bc42a94193ec" });
        }
    }
}
