using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingApp.Data.Migrations
{
	public partial class AddSettingsKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "Key", "Value" },
                values: new object[] { 7, "Account_BaseAccountNo", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 7);

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
        }
    }
}
