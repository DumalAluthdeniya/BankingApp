using BankingApp.Areas.BackOffice.Models;
using BankingApp.Data;
using BankingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sentry;
using System.Data;
using System.Security.Principal;

namespace BankingApp.Areas.BackOffice.Controllers
{
	[Authorize]
	[HasPermission("Manage Support")]
	[Area("BackOffice")]
	[Route("BackOffice/[controller]")]
	public class SupportController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly ILogger<ExchangeController> _logger;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IWebHostEnvironment _hostingEnvironment;


		public SupportController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, ILogger<ExchangeController> logger, UserManager<ApplicationUser> userManager, IWebHostEnvironment hostingEnvironment)
		{
			_context = context;
			_roleManager = roleManager;
			_logger = logger;
			_userManager = userManager;
			_hostingEnvironment = hostingEnvironment;
		}

		public async Task<IActionResult> Index(int activeTab = 1, int ticketId = 0)
		{
			try
			{

				ViewBag.TicketId = ticketId;

				return View(new SupportsViewModel()
				{
					Supports = await _context.Supports.Include(s => s.Customer).Include(s => s.CreatedBy).OrderByDescending(s => s.CreatedDate).ToListAsync(),
					ActiveTab = activeTab,
					Support = new Support(),
					Customers = await GetCustomersSelectListItems(string.Empty)

				});
			}
			catch (Exception e)
			{
				_logger.LogError(e.Message);
				return View();
			}
		}

		private async Task<List<SelectListItem>> GetCustomersSelectListItems(string id)
		{
			var managers = await GetUsersInRole("Customer");
			var managersItems = new List<SelectListItem>();
			foreach (var manager in managers)
			{
				var name = manager.FirstName + " " + manager.LastName;
				managersItems.Add(new SelectListItem() { Text = name, Value = manager.Id, Selected = id == manager.Id });
			}

			return managersItems;
		}

		private async Task<List<ApplicationUser>> GetUsersInRole(string roleName)
		{

			var users = await (from user in _context.ApplicationUsers
							   join userRole in _context.UserRoles
							   on user.Id equals userRole.UserId
							   join role in _context.Roles
							   on userRole.RoleId equals role.Id
							   where role.Name.Equals(roleName)
							   select user)
								 .ToListAsync();
			return users;
		}


		[HttpGet("LoadTicketDetails")]
		public async Task<IActionResult> LoadTicketDetails(int ticketId)
		{
			try
			{
				var items = await _context.SupportItems
					.Include(s => s.Support)
					.ThenInclude(s => s.Customer)
					.Include(s => s.Sender)
					.Include(s => s.Files)
					.Where(b => b.Support.Id == ticketId)
					.OrderByDescending(d => d.SendDate)
					.ToListAsync();

				return PartialView("_SupportItemsPartial", items);
			}
			catch (Exception)
			{
				throw;
			}
		}

		[HttpGet("LoadTickets")]
		public async Task<IActionResult> LoadTickets()
		{
			try
			{

				return PartialView("_SupportTicketsPartial", new SupportsViewModel()
				{
					Supports = await _context.Supports.Include(s => s.Customer).Include(s => s.CreatedBy).OrderByDescending(s => s.CreatedDate).ToListAsync(),
				}
);
			}
			catch (Exception)
			{
				throw;
			}
		}

		[HttpPost("Reply")]
		public async Task<IActionResult> Reply(int sId, string message, List<IFormFile> files)
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

				var supportFiles = new List<SupportFile>();

				if (files != null && files.Any())
				{
					var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, @"MyStaticFiles\SupportFiles");
					if (!Directory.Exists(filePath))
					{
						Directory.CreateDirectory(filePath);
					}

					foreach (var f in files)
					{
						var supportFile = new SupportFile()
						{
							SupportItem = i,
						};
						var fileName = Path.GetFileName(f.FileName);
						var fileUploadPath = Path.Combine(filePath, fileName);
						await using (Stream fileStream = new FileStream(fileUploadPath, FileMode.Create))
						{
							await f.CopyToAsync(fileStream);
						}
						supportFile.Path = fileName;
						supportFiles.Add(supportFile);
					}
				}

				i.Files = supportFiles;

				await _context.SupportItems.AddAsync(i);
				await _context.SaveChangesAsync();
				return RedirectToAction("LoadTicketDetails", new { ticketId = sId });


			}
			catch (Exception)
			{
				throw;
			}
		}

		[HttpPost("AddSupportTicket")]
		public async Task<IActionResult> AddSupportTicket(SupportsViewModel model)
		{
			var customer = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Id.Equals(model.SelectedCustomer));
			var tickets = await _context.Supports.ToListAsync();

			var ticketNo = 0;

			if (tickets != null && tickets.Count > 1)
			{
				var lastTicket = tickets.LastOrDefault();
				ticketNo = lastTicket != null ? (lastTicket.Id + 1) : 1;
			}

			model.Support.Ticket = "SIF" + ticketNo.ToString().PadLeft(4, '0');
			model.Support.Customer = customer;
			model.Support.CreatedBy = await _userManager.GetUserAsync(HttpContext.User);

			var sItem = new SupportItem()
			{
				Support = model.Support,
				IsFromSupport = true,
				Message = model.Message,
				SendDate = DateTime.Now,
				Sender = await _userManager.GetUserAsync(HttpContext.User)
			};
			var supportFiles = new List<SupportFile>();

			if (model.Files != null && model.Files.Any())
			{
				var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, @"MyStaticFiles\SupportFiles");
				if (!Directory.Exists(filePath))
				{
					Directory.CreateDirectory(filePath);
				}

				foreach (var f in model.Files)
				{
					var supportFile = new SupportFile()
					{
						SupportItem = sItem,
					};
					var fileName = Path.GetFileName(f.FileName);
					var fileUploadPath = Path.Combine(filePath, fileName);
					await using (Stream fileStream = new FileStream(fileUploadPath, FileMode.Create))
					{
						await f.CopyToAsync(fileStream);
					}
					supportFile.Path = fileName;
					supportFiles.Add(supportFile);
				}
			}

			sItem.Files = supportFiles;

			await _context.AddAsync(sItem);
			await _context.SaveChangesAsync();


			return RedirectToAction("Index", new { activeTab = 2, ticketId = model.Support.Id });
		}

		[HttpGet("Customers")]
		public async Task<ActionResult> Customers(string term)
		{
			var managers = await GetUsersInRole("Customer");
			var routeList = managers
				.Where(r => r.FirstName.ToLower().Contains(term) || r.LastName.ToLower().Contains(term))
		   .Take(5)
		   .Select(r => new { id = r.Id, label = r.FirstName + " " + r.LastName + "-" + r.CustomerUniqueId, name = "Customer" });
			return Json(routeList);

		}

		[HttpPost("CloseTicket")]
		public async Task<IActionResult> CloseTicket(int tId)
		{
			var ticket = await _context.Supports.FirstOrDefaultAsync(t => t.Id == tId);
			ticket.IsActive = !ticket.IsActive;
			await _context.SaveChangesAsync();
			return Json(ticket.IsActive);

		}
	}
}
