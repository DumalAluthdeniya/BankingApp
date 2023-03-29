using BankingApp.Data;
using BankingApp.Models;
using BankingApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            if (currentUser != null)
			{
                var user = await _userManager.FindByIdAsync(currentUser.Id);
                if (await _userManager.IsInRoleAsync(user, "Employer"))
                {
                    return RedirectToAction("Index", "Dashboard", new { Area = "BackOffice" });
				}
                if (await _userManager.IsInRoleAsync(user, "Customer"))
                {

                    var model = new HomeViewModel()
                    {
                        Accounts = await _context.Accounts.Where(a => a.Customer == currentUser).ToListAsync(),
                        Loans = await _context.Loans.Where(a => a.Customer == currentUser).ToListAsync(),
                        User  = currentUser
                    };
                    return View(model);
                }
            }
            return View();
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