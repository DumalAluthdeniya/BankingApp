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
	[HasPermission("Manage Dashboard")]
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

		[HttpGet("GetTransactions")]
		public async Task<IActionResult> GetTransactions()
		{
			try
			{
				var transactions = await _context.Transactions
					.Include(t => t.Account)
					.Where(t =>
								t.CreatedDate >= DateTime.Now.AddMonths(-5)
							&& (t.TransactionType == TransactionType.Deposit || t.TransactionType == TransactionType.Withdrawal))
					.ToListAsync();

				var deposits = transactions.FindAll(t => t.TransactionType == TransactionType.Deposit);
				var withdrawals = transactions.FindAll(t => t.TransactionType == TransactionType.Withdrawal);

				var months = Enumerable.Range(0, 6)
							  .Select(i => DateTime.Now.AddMonths(i - 4))
							  .Select(date => date.ToString("MMM")).ToArray();



				var depositsGrouped = from t in deposits
									  group t by new
									  {
										  t.CreatedDate.Month
									  } into g
									  select new
									  {
										  Month = g.Key.Month,
										  Total = g.Sum(t => t.Amount)
									  };

				var withdrawalsGrouped = from t in withdrawals
										 group t by new
										 {
											 t.CreatedDate.Month
										 } into g
										 select new
										 {
											 Month = g.Key.Month,
											 Total = g.Sum(t => Math.Abs(t.Amount))
										 };

				var savingAccountDeposits = transactions.Where(t => t.TransactionType == TransactionType.Deposit && t.Account.AccountType == AccountType.Savings).Sum(t => t.Amount);
				var spendingAccountDeposits = transactions.Where(t => t.TransactionType == TransactionType.Deposit && t.Account.AccountType == AccountType.Spending).Sum(t => t.Amount);

				var typeDeposits = new List<decimal>
				{
					savingAccountDeposits,
					spendingAccountDeposits
				};

				return Json(new Chart()
				{
					Deposits = depositsGrouped.Select(d => d.Total).ToArray(),
					Withdrawals = withdrawalsGrouped.Select(d => d.Total).ToArray(),
					Months = Enumerable.Range(1, 5)
							  .Select(i => DateTime.Now.AddMonths(i - 5))
							  .Select(date => date.ToString("MMM")).ToArray(),
					AccountTypeDeposits = typeDeposits.ToArray()
				});

			}
			catch (Exception)
			{

				throw;
			}
		}
	}
}
