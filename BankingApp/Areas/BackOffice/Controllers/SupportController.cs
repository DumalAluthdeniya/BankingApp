using BankingApp.Areas.BackOffice.Models;
using BankingApp.Controllers;
using BankingApp.Data;
using BankingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using PasswordGenerator;
using System.Data;

namespace BankingApp.Areas.BackOffice.Controllers
{
	[Authorize]
	[Area("BackOffice")]
	[Route("BackOffice/[controller]")]
	public class SupportController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly ILogger<ExchangeController> _logger;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly UserManager<ApplicationUser> _userManager;

		public SupportController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, ILogger<ExchangeController> logger, UserManager<ApplicationUser> userManager)
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
				return View(await _context.Supports.Include(s => s.Customer).ToListAsync());
			}
			catch (Exception e)
			{
				_logger.LogError(e.Message);
				return View();
			}
		}

		[Route("LoadSupportItems")]
		public async Task<IActionResult> LoadSupportItems(int supportId)
		{
			try
			{
				var items = await _context.SupportItems
					.Include(s => s.Support)
					.ThenInclude(s => s.Customer)
					.Include(s => s.Sender)
					.Where(b => b.Support.Id == supportId).ToListAsync();
				return View("SupportItems", items);
			}
			catch (Exception)
			{
				throw;
			}
		}

		[HttpPost("Reply")]
		public async Task<IActionResult> Reply(int sId, string message)
		{
			try
			{
				var i = new SupportItem()
				{
					IsFromSupport = true,
					Message = message,
					SendDate = DateTime.Now,
					Support = await _context.Supports.FirstOrDefaultAsync(f => f.Id == sId),
					Sender = await _userManager.GetUserAsync(HttpContext.User)
				};

				await _context.SupportItems.AddAsync(i);
				await _context.SaveChangesAsync();
				return Json(true);

			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
