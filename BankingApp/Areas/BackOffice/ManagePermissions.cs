using BankingApp.Data;
using BankingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Areas.BackOffice
{
	public class ManagePermissions
	{
		private static ApplicationDbContext? _context;

		public ManagePermissions(ApplicationDbContext context)
		{
			_context = context;
		}

		public static async Task<List<EmployeePermission>> Get(ApplicationUser user)
		{
			return await _context.EmployeePermissions.Include(p => p.Permission).Where(p => p.Employee == user).ToListAsync();
		}
	}
}
