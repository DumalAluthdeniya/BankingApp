using BankingApp.Areas.BackOffice.Models;
using BankingApp.Data;
using BankingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Areas.BackOffice.Controllers
{
	[Authorize]
	[Area("BackOffice")]
	[Route("BackOffice/[controller]")]
	public class DashboardController : Controller
    {
		private readonly ApplicationDbContext _context;
		private readonly ILogger<EmployersController> _logger;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly UserManager<ApplicationUser> _userManager;

		public DashboardController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<EmployersController> logger, ApplicationDbContext context)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_logger = logger;
			_context = context;
		}

		public async Task<IActionResult> Index()
        {
			var model = new DashboardViewModel()
			{
				CustomerCount = await GetUsersCountInRole("Customer"),
				TotalDeposits = await _context.Transactions.Where(t => t.TransactionType == TransactionType.Deposit).SumAsync(t => t.Amount),
				TotalLoansSum = await _context.Loans.Where(l => l.LoanStatus == LoanStatus.Completed).SumAsync(l => l.BankOfferAmount)
			};
            return View(model);
        }

		private async Task<int> GetUsersCountInRole(string roleName)
		{

			var users = await (from user in _context.ApplicationUsers
							   join userRole in _context.UserRoles
							   on user.Id equals userRole.UserId
							   join role in _context.Roles
							   on userRole.RoleId equals role.Id
							   where role.Name.Equals(roleName)
							   select user)
								 .ToListAsync();
			return users.Count;
		}
	}
}
