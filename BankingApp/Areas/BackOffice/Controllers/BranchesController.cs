using BankingApp.Areas.BackOffice.Models;
using BankingApp.Controllers;
using BankingApp.Data;
using BankingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Areas.BackOffice.Controllers
{
	[Authorize]
	[Area("BackOffice")]
	[Route("BackOffice/[controller]")]
	public class BranchesController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly ILogger<HomeController> _logger;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly UserManager<ApplicationUser> _userManager;

		public BranchesController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, ILogger<HomeController> logger, UserManager<ApplicationUser> userManager)
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
				return View(new ManageBranchesViewModel()
				{
					Branches = await _context.Branches.Include(b => b.Manager).ToListAsync(),
					Branch = new Branch(),
					Managers = await GetManagersSelectListItems(string.Empty)
				});
			}
			catch (Exception e)
			{
				_logger.LogError(e.Message);
				return View();
			}
		}

		private async Task<List<SelectListItem>> GetManagersSelectListItems(string id)
		{
			var managers = await GetUsersInRole("Employer");
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

		[HttpPost]
		public async Task<IActionResult> AddUpdateBranch(ManageBranchesViewModel model)
		{
			if (ModelState.IsValid)
			{
				var currentUser = await _userManager.GetUserAsync(HttpContext.User);
				ApplicationUser? manager = !string.IsNullOrEmpty(model.SelectedManager) ? await _context.ApplicationUsers.FirstOrDefaultAsync(m => m.Id.Equals(model.SelectedManager)) : null;

				if (model.Branch.Id > 0)
				{
					//var branch = await _context.Branches.SingleAsync(b => b.Id == model.Branch.Id);

					model.Branch.Manager = manager;
					model.Branch.UpdatedBy = currentUser;
					model.Branch.UpdatedTime = DateTime.Now;
					_context.Branches.Update(model.Branch);

				}
				else
				{
					model.Branch.Manager = manager;
					model.Branch.CreatedBy = currentUser;
					await _context.Branches.AddAsync(model.Branch);
				}

				await _context.SaveChangesAsync();
				return RedirectToAction("Index");

			}
			model.Managers = await GetManagersSelectListItems(string.Empty);
			model.ActiveTab = 3;
			return View("Index", model);
		}

		[Route("LoadBranchDetails")]
		public async Task<IActionResult> LoadBranchDetails(int branchId)
		{
			try
			{
				var branch = await _context.Branches.Include(b => b.Manager).SingleAsync(b => b.Id == branchId);
				return PartialView("_BranchDetailsPartial", new ManageBranchesViewModel()
				{
					Branch = branch,
					Managers = await GetManagersSelectListItems(branch.Manager.Id)
				});
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
