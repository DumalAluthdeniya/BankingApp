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
	public class CustomersController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly ILogger<HomeController> _logger;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly UserManager<ApplicationUser> _userManager;

		public CustomersController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, ILogger<HomeController> logger, UserManager<ApplicationUser> userManager)
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
				return View(new ManageCustomersViewModel()
				{
					Customers = await GetUsersInRole("Customer"),
					Customer = new ApplicationUser()
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
		public async Task<IActionResult> AddUpdateCustomer(ManageCustomersViewModel model)
		{
			if (ModelState.IsValid)
			{
				//var currentUser = await _userManager.GetUserAsync(HttpContext.User);

				if (!string.IsNullOrEmpty(model.Customer.Id))
				{
					//model.Customer.UpdatedBy = currentUser;
					var mUser = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == model.Customer.Id);
					mUser.UpdatedDate = DateTime.Now;
					mUser.AccountBalance = model.Customer.AccountBalance;
					mUser.LoanBalance = model.Customer.LoanBalance;
					mUser.FirstName = model.Customer.FirstName;
					mUser.LastName = model.Customer.LastName;
					mUser.Email = model.Customer.Email;
					mUser.PhoneNumber = model.Customer.PhoneNumber;
					mUser.AddressLine1 = model.Customer.AddressLine1;
					mUser.AddressLine2 = model.Customer.AddressLine2;
					mUser.City = model.Customer.City;
					mUser.Region = model.Customer.Region;
					mUser.Zip = model.Customer.Zip;
					mUser.Country = model.Customer.Country;
					mUser.IsActive = model.Customer.IsActive;
					await _context.SaveChangesAsync();

				}
				else
				{
					var user = await _userManager.FindByEmailAsync(model.Customer.Email);
					if (user != null)
					{
						ModelState.AddModelError("Duplicate Customer", "Customer with same email already exists.");
						model.ActiveTab = 3;
						return View("Index", model);
					}
					var pwd = new Password();
					var password = pwd.Next();
					model.Customer.UserName = model.Customer.Email;
					model.Customer.Id = Guid.NewGuid().ToString();
					await _userManager.CreateAsync(model.Customer, password);
					await _userManager.AddToRoleAsync(model.Customer, "Customer");
				}

				return RedirectToAction("Index");

			}
			model.ActiveTab = 3;
			return View("Index", model);
		}


		[Route("LoadCustomerDetails")]
		public async Task<IActionResult> LoadCustomerDetails(string customerId)
		{
			try
			{
				var customer = await _userManager.FindByIdAsync(customerId);
				return PartialView("_CustomerDetailsPartial", new ManageCustomersViewModel()
				{
					Customer = customer
				});
			}
			catch (Exception)
			{
				throw;
			}
		}

		[Route("LoadAccounts")]
		public async Task<IActionResult> LoadAccounts(string customerId)
		{
			try
			{
				var customer = await _userManager.FindByIdAsync(customerId);
				var accounts = await _context.Accounts.Where(a => a.Customer == customer).ToListAsync();
				var loans = await _context.Loans.Where(a => a.Customer == customer).ToListAsync();
				return PartialView("_CustomerAccountsPartial", new ManageCustomersViewModel()
				{
					Customer = customer,
					Accounts = accounts,
					Loans = loans,
					Account = new Account()

				});
			}
			catch (Exception)
			{
				throw;
			}
		}
		[Route("LoadAddAccount")]
		public async Task<IActionResult> LoadAddAccount(string customerId)
		{
			try
			{
				var currentUser = await _userManager.GetUserAsync(HttpContext.User);
				var customer = await _userManager.FindByIdAsync(customerId);
				return PartialView("_CustomerCreateAccountPartial", new ManageCustomersViewModel()
				{
					Account = new Account()
					{
						Customer = customer,
						CreatedBy = currentUser.Id
					}

				});
			}
			catch (Exception)
			{
				throw;
			}
		}

		[HttpPost("AddUpdateAccount")]
		public async Task<IActionResult> AddUpdateAccount(ManageCustomersViewModel model)
		{
			try
			{
				var customer = await _userManager.FindByIdAsync(model.Account.Customer.Id);
				model.Account.Customer = customer;
				await _context.Accounts.AddAsync(model.Account);
				await _context.SaveChangesAsync();

				var accounts = await _context.Accounts.Where(a => a.Customer == customer).ToListAsync();
				var loans = await _context.Loans.Where(a => a.Customer == customer).ToListAsync();
				return View("Index", new ManageCustomersViewModel()
				{
					Customer = customer,
					Accounts = accounts,
					Loans = loans,
					Account = new Account(),
					ActiveTab = 4
				});



			}
			catch (Exception)
			{

				throw;
			}
		}

		[Route("LoadAccountTransactions")]
		public async Task<IActionResult> LoadAccountTransactions(int accountId)
		{
			try
			{
				var acc = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);
				var trans = await _context.Transactions
					.Include(t => t.CreatedBy)
					.Include(t => t.DestinationAccount)
					.Where(t => t.AccountId == accountId)
					.ToListAsync();

				return PartialView("_CustomerAccountTransactionsPartial", new ManageCustomersViewModel()
				{
					Transaction= new Transaction(),
					Transactions = trans,
					Account = acc
				});
			}
			catch (Exception)
			{
				throw;
			}
		}

		[HttpPost("AddUpdateTransaction")]
		public async Task<IActionResult> AddUpdateTransaction(ManageCustomersViewModel model)
		{
			try
			{
				var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == model.Account.Id);
				model.Transaction.Account = account;
				model.Transaction.CreatedBy = await _userManager.GetUserAsync(HttpContext.User);

				switch (model.Transaction.TransactionType)
				{
					case TransactionType.Deposit:
						account.Balance += model.Transaction.Amount;
						model.Transaction.DestinationAccount = account;
						break;
					case TransactionType.Withdrawal:
						account.Balance -= model.Transaction.Amount;
						model.Transaction.DestinationAccount = account;
						break;
					case TransactionType.TransferToLocal:
						model.Transaction.DestinationAccount = account;

						break;
					case TransactionType.TransferToLoan:
						model.Transaction.DestinationAccount = account;

						break;
					case TransactionType.Interest:
						model.Transaction.DestinationAccount = account;
						break;
					default:
						break;
				}

				await _context.Transactions.AddAsync(model.Transaction);
				await _context.SaveChangesAsync();

				var trans = await _context.Transactions.Where(a => a.AccountId == account.Id).ToListAsync();
				return View("Index", new ManageCustomersViewModel()
				{
					Transactions = trans,
					Account = account,
					ActiveTab = 5
				});

			}
			catch (Exception)
			{

				throw;
			}
		}
	}
}
