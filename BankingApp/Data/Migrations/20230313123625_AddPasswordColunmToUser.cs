using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingApp.Data.Migrations
{
	public partial class AddPasswordColunmToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1075abc4-3191-40f3-b171-535495d8339e");

            migrationBuilder.AddColumn<string>(
                name: "DefaultPassword",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "a048d589-31fd-48f4-a674-ee7f3bf4b419");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fd0ff8e2-f1a8-4a42-835e-d4aaff7e7b8d", "01af0432-5873-4d89-a1bc-1b3b591a6b32", "Customer", "CUSTOMER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bddbecc8-5de7-4e02-9a59-4b778baa7474", new DateTime(2023, 3, 13, 23, 36, 23, 835, DateTimeKind.Local).AddTicks(2352), "AQAAAAEAACcQAAAAEJFJ6eY13EdFrWWwoXlw7qoR+c2BZXco3+Lk2QD65rLpeRZ4xz/l+xbK9VKjLCvFYA==", "6a33ea19-2c7e-4289-b5fa-db72b78e99c3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd0ff8e2-f1a8-4a42-835e-d4aaff7e7b8d");

            migrationBuilder.DropColumn(
                name: "DefaultPassword",
                table: "AspNetUsers");

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
        }
    }
}
