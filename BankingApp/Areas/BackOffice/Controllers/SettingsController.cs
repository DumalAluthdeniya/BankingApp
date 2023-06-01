using BankingApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Areas.BackOffice.Controllers
{
	[Authorize]
	[HasPermission("Manage Fee")]
	[Area("BackOffice")]
	[Route("BackOffice/[controller]")]
	public class SettingsController : Controller
	{


		private readonly ApplicationDbContext _context;
		private readonly ILogger<SettingsController> _logger;

		public SettingsController(ApplicationDbContext context, ILogger<SettingsController> logger)
		{
			_context = context;
			_logger = logger;
		}

		public async Task<IActionResult> Index()
		{
			try
			{
				return View(await _context.Settings.ToListAsync());

			}
			catch (Exception e)
			{
				return View();
			}
		}

		[HttpPost("UpdateSettingValue")]
		public async Task<IActionResult> UpdateSettingValue(int id, string value)
		{
			var eFees = await _context.Settings.FirstOrDefaultAsync(f => f.Id == id);
			eFees.Value = value;
			await _context.SaveChangesAsync();
			return Json(true);
		}


	}
}
