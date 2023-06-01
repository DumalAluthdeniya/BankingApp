using BankingApp.Data;
using BankingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Areas.BackOffice.Controllers
{
	[Authorize]
	[HasPermission("Manage Fee")]
	[Area("BackOffice")]
	[Route("BackOffice/[controller]")]
	public class FeesController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly ILogger<FeesController> _logger;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly UserManager<ApplicationUser> _userManager;

		public FeesController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, ILogger<FeesController> logger, UserManager<ApplicationUser> userManager)
		{
			_context = context;
			_roleManager = roleManager;
			_logger = logger;
			_userManager = userManager;
		}

		public async Task<IActionResult> Index()
		{
			try
			{
				return View(await _context.AccountFeeSettings.ToListAsync());
			}
			catch (Exception e)
			{
				_logger.LogError(e.Message);
				return View();
			}
		}


		[HttpPost("UpdateFeeValue")]
		public async Task<IActionResult> UpdateFeeValue(int id, string value)
		{
			var eFees = await _context.AccountFeeSettings.FirstOrDefaultAsync(f => f.Id == id);
			eFees.Value = value;
			await _context.SaveChangesAsync();
			return Json(true);
		}
	}
}
