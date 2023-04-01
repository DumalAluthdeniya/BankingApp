using BankingApp.Data;
using BankingApp.Models;
using BankingApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Controllers
{
	public class ServicesController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;

		public ServicesController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
		{
			_userManager = userManager;
			_context = context;
		}

		public async Task<IActionResult> Index(int activeTab = 1)
		{
			var currentUser = await _userManager.GetUserAsync(HttpContext.User);

			var model = new ServicesViewModel()
			{
				Loan = new Loan(),
				LoanRequests = await _context.Loans.Where(l => l.Customer == currentUser).ToListAsync(),
				ActiveTab = activeTab
			};
			return View(model);
		}

		public async Task<IActionResult> AddLoanRequestAsync(ServicesViewModel model)
		{
			try
			{
				var currentUser = await _userManager.GetUserAsync(HttpContext.User);
				model.Loan.Customer = currentUser;

				await _context.Loans.AddAsync(model.Loan);
				await _context.SaveChangesAsync();

				return RedirectToAction("Index", new { activeTab = 2 });

			}
			catch (Exception)
			{

				throw;
			}
		}
	}
}
