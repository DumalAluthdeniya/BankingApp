using BankingApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Areas.BackOffice
{
	public class HasPermission : Attribute, IAuthorizationFilter
	{
		private readonly string _permission;

		public HasPermission(string permission)
		{
			_permission = permission;
		}

		public void OnAuthorization(AuthorizationFilterContext context)
		{
			var dbContext = context.HttpContext.RequestServices.GetRequiredService<ApplicationDbContext>();
			var name = context.HttpContext.User.Identity.Name;
			var currentUser = dbContext.Users.FirstOrDefault(u => u.UserName == name);
			var permissionDb = dbContext.EmployeePermissions
				.Include(p => p.Permission)
				.Where(c => c.Employee == currentUser && c.Permission.Name.Equals(_permission)).FirstOrDefault();

			if (!permissionDb.IsActive)
			{
				context.Result = new ForbidResult();

			}
		}
	}
}
