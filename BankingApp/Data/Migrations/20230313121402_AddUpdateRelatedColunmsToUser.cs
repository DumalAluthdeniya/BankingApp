using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingApp.Data.Migrations
{
	public partial class AddUpdateRelatedColunmsToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_CreatedById",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Branches_AspNetUsers_CreatedById",
                table: "Branches");

            migrationBuilder.DropForeignKey(
                name: "FK_Branches_AspNetUsers_ManagerId",
                table: "Branches");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CreatedById",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "83780caf-39b9-4ed5-bce4-bac92675b5bc");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "ManagerId",
                table: "Branches",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Branches",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "2359fe6c-b85c-48cc-885a-693f4c549334");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1075abc4-3191-40f3-b171-535495d8339e", "829f9947-f026-49bb-b713-af5d837e7b15", "Customer", "CUSTOMER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "894d96ff-1a1c-4fe2-b90e-af42db9a6653", new DateTime(2023, 3, 13, 23, 14, 0, 727, DateTimeKind.Local).AddTicks(3449), "AQAAAAEAACcQAAAAEDbKUk6M3wyd9RCFwW8q75ownMPxpKupKAVk56WbvVOf/+oJ1DNg5HT++vDI3H5N/w==", "25d78273-26ba-48ef-b335-c652e65139ed" });

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_AspNetUsers_CreatedById",
                table: "Branches",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_AspNetUsers_ManagerId",
                table: "Branches",
                column: "ManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_AspNetUsers_CreatedById",
                table: "Branches");

            migrationBuilder.DropForeignKey(
                name: "FK_Branches_AspNetUsers_ManagerId",
                table: "Branches");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1075abc4-3191-40f3-b171-535495d8339e");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "ManagerId",
                table: "Branches",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Branches",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CreatedById",
                table: "AspNetUsers",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_CreatedById",
                table: "AspNetUsers",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_AspNetUsers_CreatedById",
                table: "Branches",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_AspNetUsers_ManagerId",
                table: "Branches",
                column: "ManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
