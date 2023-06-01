using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingApp.Data.Migrations
{
	public partial class AddBalanceColunmToTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "Transactions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "a3ce10a5-d937-4839-b352-dbb865089cde");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd0ff8e2-f1a8-4a42-835e-d4aaff7e7b8d",
                column: "ConcurrencyStamp",
                value: "a252c234-72f4-4657-a023-5049f6c19c48");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "94243e93-1e26-4ef2-aa12-3c91239322ba", new DateTime(2023, 4, 6, 19, 56, 55, 651, DateTimeKind.Local).AddTicks(81), "AQAAAAEAACcQAAAAEA4PV39+kGj2VNuTMy5VPpB9ERsGwrwpTPziptRXS1NAFKypsmeXEDmBdh1Tb7QRqQ==", "05be73a3-d346-4729-853b-bc4e4fbf57e3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Transactions");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "630b38b2-c53c-487e-9796-5db6c28e144e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd0ff8e2-f1a8-4a42-835e-d4aaff7e7b8d",
                column: "ConcurrencyStamp",
                value: "d93c35f7-97be-4c9e-b327-208197f52552");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b8805142-3308-49f5-9ab4-9b4547673a5c", new DateTime(2023, 4, 6, 18, 29, 36, 585, DateTimeKind.Local).AddTicks(4763), "AQAAAAEAACcQAAAAEOKeXv/2B3lqUV2t1dMvV3OcChup/muCI7Sar+ywe5Ly/vCX6XusCGi6q6TZQugNRQ==", "8f3c4cbc-8d2b-4563-ac3c-94c341962c77" });
        }
    }
}
