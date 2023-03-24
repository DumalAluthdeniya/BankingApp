using BankingApp.Areas.BackOffice.Models;
using BankingApp.Controllers;
using BankingApp.Data;
using BankingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PasswordGenerator;
using System.Data;

namespace BankingApp.Areas.BackOffice.Controllers
{
	[Authorize]
	[Area("BackOffice")]
	[Route("BackOffice/[controller]")]
	public class EmployersController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly ILogger<EmployersController> _logger;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly UserManager<ApplicationUser> _userManager;

		public EmployersController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, ILogger<EmployersController> logger, UserManager<ApplicationUser> userManager)
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
				return View(new ManageEmployersViewModel()
				{
					Employers = await GetUsersInRole("Employer"),
					EPermissions = await GetEmployeePermissions(),
					Employer = new ApplicationUser()
					{
						Id = string.Empty
					}
				});
			}
			catch (Exception e)
			{
				_logger.LogError(e.Message);
				return View();
			}
		}

		private async Task<List<PermissionModel>> GetEmployeePermissions()
		{
			var empPermissions = new List<PermissionModel>();
			foreach (var p in await _context.Permissions.ToListAsync())
			{
				empPermissions.Add(new PermissionModel() { Permission = p });
			}

			return empPermissions;
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
		public async Task<IActionResult> AddUpdateEmployer(ManageEmployersViewModel model)
		{
			if (ModelState.IsValid)
			{
				//var currentUser = await _userManager.GetUserAsync(HttpContext.User);

				if (!string.IsNullOrEmpty(model.Employer.Id))
				{
					//model.Customer.UpdatedBy = currentUser;
					var mUser = await _context.ApplicationUsers.Include(e => e.EmployeePermissions).FirstOrDefaultAsync(u => u.Id == model.Employer.Id);
					mUser.UpdatedDate = DateTime.Now;
					mUser.FirstName = model.Employer.FirstName;
					mUser.LastName = model.Employer.LastName;
					mUser.Email = model.Employer.Email;
					mUser.PhoneNumber = model.Employer.PhoneNumber;
					mUser.AddressLine1 = model.Employer.AddressLine1;
					mUser.AddressLine2 = model.Employer.AddressLine2;
					mUser.City = model.Employer.City;
					mUser.Region = model.Employer.Region;
					mUser.Zip = model.Employer.Zip;
					mUser.Country = model.Employer.Country;
					mUser.IsActive = true;
					var count = 0;
					foreach (var p in mUser.EmployeePermissions)
					{
						p.IsActive = model.EPermissions[count].IsSelected;
						p.PermissionType = model.EPermissions[count].SelectedPermission.Equals(PermissionType.Restricted.ToString()) ? PermissionType.Restricted : model.EPermissions[count].SelectedPermission.Equals(PermissionType.View.ToString()) ? PermissionType.View : PermissionType.Update;
						count++;
					}
					await _context.SaveChangesAsync();

				}
				else
				{
					var user = await _userManager.FindByEmailAsync(model.Employer.Email);
					if (user != null)
					{
						ModelState.AddModelError("Duplicate Customer", "Customer with same email already exists.");
						model.ActiveTab = 3;
						return View("Index", model);
					}
					var pwd = new Password();
					var password = pwd.Next();
					model.Employer.UserName = model.Employer.Email;
					model.Employer.Id = Guid.NewGuid().ToString();

					var employerPermissions = new List<EmployeePermission>();
					foreach (var p in model.EPermissions)
					{
						employerPermissions.Add(new EmployeePermission()
						{
							Employee = model.Employer,
							Permission = await _context.Permissions.SingleAsync(pp => pp.Id == p.Permission.Id),
							IsActive = p.IsSelected,
							PermissionType = p.SelectedPermission == null ? PermissionType.Restricted : p.SelectedPermission.Equals(PermissionType.View.ToString()) ? PermissionType.View : PermissionType.Update
						});
					}
					model.Employer.EmployeePermissions = employerPermissions;
					await _userManager.CreateAsync(model.Employer, password);
					await _userManager.AddToRoleAsync(model.Employer, "Employer");
				}

				return RedirectToAction("Index");

			}
			model.ActiveTab = 3;
			return View("Index", model);
		}


		[Route("LoadEmployerDetails")]
		public async Task<IActionResult> LoadEmployerDetails(string employerId)
		{
			try
			{
				var emp = await _context.ApplicationUsers.Include(e => e.EmployeePermissions).FirstOrDefaultAsync(e => e.Id == employerId);
				var empAssignedPermissions = emp.EmployeePermissions.ToList();
				var empPermissions = new List<PermissionModel>();
				var count = 0;
				foreach (var p in await _context.Permissions.ToListAsync())
				{
					empPermissions.Add(new PermissionModel()
					{
						Permission = p,
						IsSelected = empAssignedPermissions[count].IsActive,
						SelectedPermission = empAssignedPermissions[count].PermissionType == PermissionType.Restricted ? PermissionType.Restricted.ToString() : empAssignedPermissions[count].PermissionType == PermissionType.View ? PermissionType.View.ToString() : PermissionType.Update.ToString()
					});
					count++;
				}
				return PartialView("_EmployerDetailsPartial", new ManageEmployersViewModel()
				{
					EPermissions = empPermissions,
					Employer = emp
				}); ;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
