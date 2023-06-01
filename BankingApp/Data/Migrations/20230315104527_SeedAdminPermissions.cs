using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingApp.Data.Migrations
{
	public partial class SeedAdminPermissions : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql("INSERT INTO EmployeePermissions VALUES ('8e445865-a24d-4543-a6c6-9443d048cdb9',1,1,1)");
			migrationBuilder.Sql("INSERT INTO EmployeePermissions VALUES ('8e445865-a24d-4543-a6c6-9443d048cdb9',2,1,1)");
			migrationBuilder.Sql("INSERT INTO EmployeePermissions VALUES ('8e445865-a24d-4543-a6c6-9443d048cdb9',3,1,1)");
			migrationBuilder.Sql("INSERT INTO EmployeePermissions VALUES ('8e445865-a24d-4543-a6c6-9443d048cdb9',4,1,1)");
			migrationBuilder.Sql("INSERT INTO EmployeePermissions VALUES ('8e445865-a24d-4543-a6c6-9443d048cdb9',5,1,1)");
			migrationBuilder.Sql("INSERT INTO EmployeePermissions VALUES ('8e445865-a24d-4543-a6c6-9443d048cdb9',6,1,1)");
			migrationBuilder.Sql("INSERT INTO EmployeePermissions VALUES ('8e445865-a24d-4543-a6c6-9443d048cdb9',7,1,1)");
			migrationBuilder.Sql("INSERT INTO EmployeePermissions VALUES ('8e445865-a24d-4543-a6c6-9443d048cdb9',8,1,1)");
			migrationBuilder.Sql("INSERT INTO EmployeePermissions VALUES ('8e445865-a24d-4543-a6c6-9443d048cdb9',9,1,1)");
			migrationBuilder.Sql("INSERT INTO EmployeePermissions VALUES ('8e445865-a24d-4543-a6c6-9443d048cdb9',10,1,1)");
			migrationBuilder.Sql("INSERT INTO EmployeePermissions VALUES ('8e445865-a24d-4543-a6c6-9443d048cdb9',11,1,1)");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql("DELETE FROM EmployeePermissions WHERE EmployeeId = '8e445865-a24d-4543-a6c6-9443d048cdb9'");
		}
	}
}
