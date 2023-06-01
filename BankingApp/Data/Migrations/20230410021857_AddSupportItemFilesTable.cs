using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingApp.Data.Migrations
{
	public partial class AddSupportItemFilesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Area",
                table: "Supports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Supports",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SupportFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupportItemId = table.Column<int>(type: "int", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupportFiles_SupportItems_SupportItemId",
                        column: x => x.SupportItemId,
                        principalTable: "SupportItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "0b52cd72-3482-4601-bb47-775eb374a113");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd0ff8e2-f1a8-4a42-835e-d4aaff7e7b8d",
                column: "ConcurrencyStamp",
                value: "5ff701fb-8ed1-4d11-9017-e8a091745944");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "22fd825f-7edd-4814-89f5-60fbecb4b2aa", new DateTime(2023, 4, 10, 12, 18, 55, 33, DateTimeKind.Local).AddTicks(8949), "AQAAAAEAACcQAAAAEHXPjt9lK04798R/ZWRISHwuJFOpeURhR+B+KbedspZhyDZVcJESprQ6dcDE4jZByA==", "30bcdb17-23e5-403e-b968-4f4cb138901c" });

            migrationBuilder.CreateIndex(
                name: "IX_Supports_CreatedById",
                table: "Supports",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SupportFiles_SupportItemId",
                table: "SupportFiles",
                column: "SupportItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Supports_AspNetUsers_CreatedById",
                table: "Supports",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Supports_AspNetUsers_CreatedById",
                table: "Supports");

            migrationBuilder.DropTable(
                name: "SupportFiles");

            migrationBuilder.DropIndex(
                name: "IX_Supports_CreatedById",
                table: "Supports");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "Supports");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Supports");

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
    }
}
