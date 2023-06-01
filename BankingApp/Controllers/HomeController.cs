using BankingApp.Data;
using BankingApp.Models;
using BankingApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Sentry.Protocol;
using System;
using System.Diagnostics;

namespace BankingApp.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly ApplicationDbContext _context;

		public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
		{
			_logger = logger;
			_userManager = userManager;
			_roleManager = roleManager;
			_context = context;
		}
		public async Task<IActionResult> Index()
		{
			try
			{
				var currentUser = await _userManager.GetUserAsync(HttpContext.User);
				if (currentUser != null)
				{
					var user = await _userManager.FindByIdAsync(currentUser.Id);

					string controller = await GetController(currentUser);

					if (controller is null)
						await _userManager.UpdateSecurityStampAsync(user);

						//return RedirectToPage("/Account/Logout", new { Area = "Identity", returnUrl = "Index", "Home", new { area = "" }) });

					if (await _userManager.IsInRoleAsync(user, "Employer"))
					{
						return RedirectToAction("Index", controller, new { Area = "BackOffice" });
					}
					if (await _userManager.IsInRoleAsync(user, "Customer"))
					{

						var model = new HomeViewModel()
						{
							Accounts = await _context.Accounts.Where(a => a.Customer == currentUser).ToListAsync(),
							Loans = await _context.Loans.Where(a => a.Customer == currentUser).ToListAsync(),
							User = currentUser
						};
						return View(model);
					}
				}
			}
			catch (Exception e)
			{
				_logger.LogError(e.Message);
				throw;
			}

			return View();
		}

		private async Task<string?> GetController(ApplicationUser currentUser)
		{
			var permission = await _context.EmployeePermissions
									.Include(p => p.Permission)
									.Where(c => c.Employee == currentUser && c.IsActive)
									.OrderBy(p => p.Permission.SequenceNo)
									.FirstOrDefaultAsync();
			if (permission is null)
				return null;

			var controller = string.Empty;

			switch (permission.Permission.Name)
			{
				case "Manage Customers":
					controller = "Customers";
					break;
				case "Manage Loan":
					controller = "Loans";
					break;
				case "Manage Fee":
					controller = "Fees";
					break;
				case "Manage Support":
					controller = "Support";
					break;
				case "Manage Exchange":
					controller = "Exchange";
					break;
				case "Manage Employees":
					controller = "Employers";
					break;
				case "Manage Cards":
					controller = "Cards";
					break;
				case "Manage Dashboard":
					controller = "Dashboard";
					break;
				case "Manage Branches":
					controller = "Branches";
					break;
				case "Manage Settlements":
					controller = "Settlements";
					break;
				case "Manage Post Updates":
					controller = "Customers";
					break;
				default:
					break;
			}

			return controller;
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}