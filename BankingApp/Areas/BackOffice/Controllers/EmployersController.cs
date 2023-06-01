using BankingApp.Areas.BackOffice.Models;
using BankingApp.Controllers;
using BankingApp.Data;
using BankingApp.Models;
using BankingApp.Utils;
using BankingApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using PasswordGenerator;
using Sentry;
using System.Data;
using System.Security;

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
		private readonly IWebHostEnvironment _hostingEnvironment;
		private readonly IEmailSender _emailSender;
		private readonly IConfiguration _configuration;



		public EmployersController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, ILogger<EmployersController> logger, UserManager<ApplicationUser> userManager, IWebHostEnvironment hostingEnvironment, IEmailSender emailSender, IConfiguration configuration)
		{
			_context = context;
			_roleManager = roleManager;
			_logger = logger;
			_userManager = userManager;
			_hostingEnvironment = hostingEnvironment;
			_emailSender = emailSender;
			_configuration = configuration;
		}

		[HasPermission("Manage Employees")]
		public async Task<IActionResult> Index(int activeTab = 1)
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
					},
					ActiveTab = activeTab

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
			if (!ModelState.IsValid && !string.IsNullOrEmpty(model.Employer.Id))
			{
				model.ActiveTab = 2;
				return View("Index", model);
			}
			else if (!ModelState.IsValid && string.IsNullOrEmpty(model.Employer.Id))
			{
				model.ActiveTab = 3;
				return View("Index", model);
			}



			if (!string.IsNullOrEmpty(model.Employer.Id))
			{
				//model.Customer.UpdatedBy = currentUser;
				var mUser = await _context.ApplicationUsers.Include(e => e.EmployeePermissions).FirstOrDefaultAsync(u => u.Id == model.Employer.Id);
				mUser.UpdatedDate = DateTime.Now;
				mUser.FirstName = model.Employer.FirstName;
				mUser.LastName = model.Employer.LastName;
				mUser.Email = model.Email;
				mUser.PhoneNumber = model.PhoneNumber;
				mUser.AddressLine1 = model.Employer.AddressLine1;
				mUser.AddressLine2 = model.Employer.AddressLine2;
				mUser.City = model.Employer.City;
				mUser.Region = model.Employer.Region;
				mUser.Zip = model.Employer.Zip;
				mUser.Country = model.Employer.Country;
				mUser.IsActive = model.Employer.IsActive;
				mUser.DefaultPassword = model.Password;

				var count = 0;
				foreach (var p in mUser.EmployeePermissions)
				{
					p.IsActive = model.EPermissions[count].IsSelected;
					p.PermissionType = model.EPermissions[count].SelectedPermission == null || model.EPermissions[count].SelectedPermission.Equals(PermissionType.Restricted.ToString()) ? PermissionType.Restricted : model.EPermissions[count].SelectedPermission.Equals(PermissionType.View.ToString()) ? PermissionType.View : PermissionType.Update;
					count++;
				}
				await _context.SaveChangesAsync();

				if (!string.IsNullOrEmpty(model.Password))
				{
					var token = await _userManager.GeneratePasswordResetTokenAsync(mUser);
					var result = await _userManager.ResetPasswordAsync(mUser, token, model.Password);
				}

				return RedirectToAction("Index");
			}
			else
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user != null)
				{
					//ModelState.AddModelError("Duplicate Customer", "Customer with same email already exists.");
					model.ActiveTab = 3;
					ViewBag.Error = "Customer with same email already exists.";
					return View("Index", model);
				}
				//var pwd = new Password();
				//var password = pwd.Next();
				model.Employer.UserName = model.Email;
				model.Employer.Email = model.Email;
				model.Employer.PhoneNumber = model.PhoneNumber;
				model.Employer.Id = Guid.NewGuid().ToString();
				model.Employer.DefaultPassword = model.Password;

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
				var identityResult = await _userManager.CreateAsync(model.Employer, model.Password);
				var result = await _userManager.AddToRoleAsync(model.Employer, "Employer");

				await SendEmailConfirmation(model.Employer);

				return RedirectToAction("Index");
			}
		}

		private async Task<bool> SendEmailConfirmation(ApplicationUser user)
		{
			try
			{
				var emailTemplate = Path.Combine(_hostingEnvironment.WebRootPath, "Templates", "UserCreationConfirmation.html");

				var builder = new BodyBuilder();
				using (var sourceReader = System.IO.File.OpenText(emailTemplate))
				{
					builder.HtmlBody = await sourceReader.ReadToEndAsync();
				}

				var appUrl = await _context.Settings.FirstOrDefaultAsync(c => c.Key.Equals("Other_ApplicationUrl"));

				var messageBody = builder.HtmlBody;
				messageBody = messageBody.Replace("fullName", user.FirstName + " " + user.LastName);
				messageBody = messageBody.Replace("userName", user.Email);
				messageBody = messageBody.Replace("userPassword", user.DefaultPassword);
				messageBody = messageBody.Replace("appUrl", appUrl.Value);

				var smtpServer = await _context.Settings.FirstOrDefaultAsync(c => c.Key.Equals("Email_SMTPServer"));
				var smtpUser = await _context.Settings.FirstOrDefaultAsync(c => c.Key.Equals("Email_SMTPUser"));
				var smtpPass = await _context.Settings.FirstOrDefaultAsync(c => c.Key.Equals("Email_SMTPPassword"));

				var es = new EmailSender(smtpServer.Value, smtpUser.Value, smtpPass.Value);

				await es.SendEmailAsync(user.Email, "SIF Employer Account Created", messageBody);

				return true;
			}
			catch (Exception ee)
			{
				_logger.LogError(ee, ee.Message);
			}
			return false;
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
					Employer = emp,
					Email = emp.Email,
					PhoneNumber = emp.PhoneNumber
				}); ;
			}
			catch (Exception)
			{
				throw;
			}
		}

		[Route("/Profile")]
		public async Task<IActionResult> Profile()
		{
			var currentUser = await _userManager.GetUserAsync(HttpContext.User);
			return View("Profile", new ManageEmployersViewModel()
			{
				Employer = currentUser
			});
		}

		[HttpPost("UpdateProfile")]
		public async Task<IActionResult> UpdateProfile(ManageEmployersViewModel model)
		{
			try
			{
				var emp = await _context.ApplicationUsers.FirstOrDefaultAsync(e => e.Id == model.Employer.Id);
				emp.FirstName = model.Employer.FirstName;
				emp.LastName = model.Employer.LastName;
				emp.Email = model.Employer.Email;
				emp.PhoneNumber = model.Employer.PhoneNumber;
				emp.AddressLine1 = model.Employer.AddressLine1;
				emp.AddressLine2 = model.Employer.AddressLine2;
				emp.City = model.Employer.City;
				emp.Region = model.Employer.Region;
				emp.Zip = model.Employer.Zip;
				emp.Country = model.Employer.Country;

				await _context.SaveChangesAsync();

				if (!string.IsNullOrEmpty(model.Password))
				{
					var token = await _userManager.GeneratePasswordResetTokenAsync(emp);
					var result = await _userManager.ResetPasswordAsync(emp, token, model.Password);
				}

				return RedirectToAction("Profile");
			}
			catch (Exception)
			{

				throw;
			}
		}

		[HttpPost("ChangeProfilePicture")]
		public async Task<IActionResult> ChangeProfilePicture(IFormFile profilePic)
		{
			var currentUser = await _userManager.GetUserAsync(HttpContext.User);
			var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Id.Equals(currentUser.Id));
			var imagePath = Path.Combine(_hostingEnvironment.ContentRootPath, @"MyStaticFiles\ProfilePictures");

			if (!Directory.Exists(imagePath))
			{
				Directory.CreateDirectory(imagePath);
			}

			if (profilePic != null)
			{
				var existingImage = imagePath + "/" + user.ProfileImage;

				if (System.IO.File.Exists(existingImage))
				{
					System.IO.File.Delete(existingImage);
				}

				var fileName = Guid.NewGuid().ToString() + Path.GetExtension(profilePic.FileName);
				var filePath = Path.Combine(imagePath, fileName);
				await using (Stream fileStream = new FileStream(filePath, FileMode.Create))
				{
					await profilePic.CopyToAsync(fileStream);
				}
				user.ProfileImage = fileName;
				await _context.SaveChangesAsync();
				return Json(true);
			}
			else
				return Json(false);
		}

		[HttpGet("LoadNewEmployer")]
		public async Task<IActionResult> LoadNewEmployer()
		{
			return PartialView("_NewEmployerPartial", new ManageEmployersViewModel()
			{
				EPermissions = await GetEmployeePermissions(),
				Employer = new ApplicationUser()
				{
					Id = string.Empty
				}
			});
		}


		[HttpPost("IsEmailTaken")]
		public async Task<IActionResult> IsEmailTaken(string email)
		{
			var user = await _userManager.FindByNameAsync(email);
			var userEmail = await _userManager.FindByEmailAsync(email);
			return user == null && userEmail == null ? Json(true) : Json(false);
		}

		[HttpPost("IsPhoneNoTaken")]
		public async Task<IActionResult> IsPhoneNoTaken(string phoneno)
		{
			var user = await _context.ApplicationUsers.FirstOrDefaultAsync(a => a.PhoneNumber.Equals(phoneno));
			return user == null ? Json(true) : Json(false);
		}
	}

}
