using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingApp.Data.Migrations
{
	public partial class AddSenderToSuppotItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SenderId",
                table: "SupportItems",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "0c17abfd-a7ae-4cbc-be9c-0ca671c71d49");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd0ff8e2-f1a8-4a42-835e-d4aaff7e7b8d",
                column: "ConcurrencyStamp",
                value: "8dcbca5d-9c59-43fb-aecf-7f54c74e5935");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "720a936e-8d8d-40c2-a9ba-9fc0efe4214a", new DateTime(2023, 3, 16, 23, 30, 19, 578, DateTimeKind.Local).AddTicks(4108), "AQAAAAEAACcQAAAAEFthDv1xAIwunHWEk+dSqj2zmHxrIt2Au03GOqhEbM7EAGJXtPiz0AQNG0XFkM+Sew==", "b4aebdd4-ddc0-4e60-894f-7031a9204016" });

            migrationBuilder.CreateIndex(
                name: "IX_SupportItems_SenderId",
                table: "SupportItems",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupportItems_AspNetUsers_SenderId",
                table: "SupportItems",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupportItems_AspNetUsers_SenderId",
                table: "SupportItems");

            migrationBuilder.DropIndex(
                name: "IX_SupportItems_SenderId",
                table: "SupportItems");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "SupportItems");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "ef8321cc-ea18-4e1c-bb41-525c1b66b0bc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd0ff8e2-f1a8-4a42-835e-d4aaff7e7b8d",
                column: "ConcurrencyStamp",
                value: "48e1551e-0f9d-46bc-893e-7ec11ab5ae8f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3e1409fe-e474-4abf-9da3-901e0e6b2471", new DateTime(2023, 3, 16, 21, 33, 38, 839, DateTimeKind.Local).AddTicks(9856), "AQAAAAEAACcQAAAAEM9WO/iMm9Nz1/DkDiZuaoONHYpG+u+6kqoqboqW4Il696FOdjehtg0P+kfysASgLg==", "76659bd3-19f4-431b-88f7-8c204e420c0f" });
        }
    }
}
