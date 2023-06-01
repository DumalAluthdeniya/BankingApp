using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingApp.Data.Migrations
{
    public partial class AddCutomerContactsTabl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ContactUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_AspNetUsers_ContactUserId",
                        column: x => x.ContactUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contacts_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "8ede4dd1-8eb8-46da-9150-4943eb0ff992");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd0ff8e2-f1a8-4a42-835e-d4aaff7e7b8d",
                column: "ConcurrencyStamp",
                value: "648dabb6-1050-4d61-8dfe-a9f8ea7780f6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9569c18e-4d6e-47d3-bb80-c99a622ddabd", new DateTime(2023, 5, 6, 13, 42, 26, 555, DateTimeKind.Local).AddTicks(9300), "AQAAAAEAACcQAAAAEEkpPW3d9/bmEkGs3EIZANw/Nz3Pz3zCncR82UdiEde7miL7sUp4SGy+6oI3trz4ew==", "63c4617e-73f1-4c96-8fdd-3b48f7f378fb" });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ContactUserId",
                table: "Contacts",
                column: "ContactUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CustomerId",
                table: "Contacts",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "b5822b6f-7615-4c32-b5f7-6666a5d843d7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd0ff8e2-f1a8-4a42-835e-d4aaff7e7b8d",
                column: "ConcurrencyStamp",
                value: "5d4c0b9e-567c-4e11-9e77-11b060374565");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4624bb7a-4073-4cb0-9d10-4cd368be5ce1", new DateTime(2023, 5, 3, 13, 48, 33, 925, DateTimeKind.Local).AddTicks(4608), "AQAAAAEAACcQAAAAEIwzSkR1KHFY3D4Dkyl0ILRSVv+sNID7NetEM0zsiFgjztkJM2ohFCjr4JOcsljJBQ==", "29c07dd6-d909-4e47-baaf-07067b3a9406" });
        }
    }
}
