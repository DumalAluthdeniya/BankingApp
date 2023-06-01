using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingApp.Data.Migrations
{
	public partial class AddSettingsTableAndData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

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

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "Key", "Value" },
                values: new object[,]
                {
                    { 1, "Exchange_BaseCurrency", null },
                    { 2, "Exchange_CurrencyList", null },
                    { 3, "Exchange_ApiLayerApiKey", null },
                    { 4, "Email_SMTPServer", null },
                    { 5, "Email_SMTPUser", null },
                    { 6, "Email_SMTPPassword", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");

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
    }
}
