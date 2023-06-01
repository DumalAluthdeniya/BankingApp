using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingApp.Data.Migrations
{
	public partial class AddExchangeHistoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExchangeId",
                table: "Exchanges");

            migrationBuilder.CreateTable(
                name: "ExchangeHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExchangeId = table.Column<int>(type: "int", nullable: false),
                    MarketRate = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    SellingRate = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    BuyingRate = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExchangeHistory_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExchangeHistory_Exchanges_ExchangeId",
                        column: x => x.ExchangeId,
                        principalTable: "Exchanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "42097813-c03c-4609-a10b-0f3d7d2e9ea2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd0ff8e2-f1a8-4a42-835e-d4aaff7e7b8d",
                column: "ConcurrencyStamp",
                value: "3ce5bf0a-ce85-4aca-81eb-bf806b6cd30e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "50095fe3-7ac4-40ea-a4a1-234fba3eceaf", new DateTime(2023, 4, 27, 12, 37, 14, 471, DateTimeKind.Local).AddTicks(1271), "AQAAAAEAACcQAAAAEM4NFiH+9yNfdQm1BCHIjt74zPREz3XfeFsUpKFlU1fcnqst7D+HCLwQWQPSRYX5SA==", "f992c6ba-d194-4f49-9994-b7437de73ac0" });

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeHistory_ExchangeId",
                table: "ExchangeHistory",
                column: "ExchangeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeHistory_UpdatedById",
                table: "ExchangeHistory",
                column: "UpdatedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExchangeHistory");

            migrationBuilder.AddColumn<int>(
                name: "ExchangeId",
                table: "Exchanges",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "35650215-12a3-45a4-bc4d-64f4139ec46e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd0ff8e2-f1a8-4a42-835e-d4aaff7e7b8d",
                column: "ConcurrencyStamp",
                value: "d981faf9-4d57-48c8-8cd3-642ba80ef1ab");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c0341460-89df-453d-834e-c1c75fb25813", new DateTime(2023, 4, 11, 12, 44, 33, 431, DateTimeKind.Local).AddTicks(2687), "AQAAAAEAACcQAAAAEMi3Jogd5xVILT5yDzTOgZdFlZBggvPY/tHmUnlb3fdU5J5J1spfDOTPDnRBzZiSBg==", "a8fdc72a-c7ff-47af-a554-0feb21c6bdb9" });
        }
    }
}
