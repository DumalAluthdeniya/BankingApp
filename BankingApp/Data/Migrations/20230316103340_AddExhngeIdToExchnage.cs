using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingApp.Data.Migrations
{
	public partial class AddExhngeIdToExchnage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExchangeId",
                table: "Exchanges");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "169a2d5f-ca13-4e8a-87ff-229da5413dba");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd0ff8e2-f1a8-4a42-835e-d4aaff7e7b8d",
                column: "ConcurrencyStamp",
                value: "1954845a-8252-4373-9b19-32ac74df6174");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "148efe17-4ac9-4705-915f-50cce7157801", new DateTime(2023, 3, 15, 21, 45, 25, 734, DateTimeKind.Local).AddTicks(437), "AQAAAAEAACcQAAAAEN93XtNxAUpBm/KIrCflHzH3mM4CyBpw8n3rrZLlptWFV/F+uKbvqlillsS/bX27rw==", "41489e59-030f-4dd5-9ba9-ccf2b63abedd" });
        }
    }
}
