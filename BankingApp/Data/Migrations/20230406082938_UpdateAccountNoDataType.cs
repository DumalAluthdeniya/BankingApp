using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingApp.Data.Migrations
{
	public partial class UpdateAccountNoDataType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "AccountNo",
                table: "Accounts",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AccountNo",
                table: "Accounts",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "2cfbc44c-5c55-4fb4-b036-f871ea026863");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd0ff8e2-f1a8-4a42-835e-d4aaff7e7b8d",
                column: "ConcurrencyStamp",
                value: "728307b9-2091-488e-bc47-fa4669d022bd");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "370d4d44-65fb-4a4e-82ab-66623502ab9d", new DateTime(2023, 4, 6, 18, 11, 40, 167, DateTimeKind.Local).AddTicks(3814), "AQAAAAEAACcQAAAAEPyiNGXk5x7ji57QA2TRCFN0oU5ZLWTwvWi62Lu2i2Mof3ykwh2K2sKROUX6raY2Tw==", "0486bd51-b24a-4cb5-bd54-f16920b7ce3e" });
        }
    }
}
