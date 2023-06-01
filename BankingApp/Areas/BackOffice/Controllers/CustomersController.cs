using BankingApp.Areas.BackOffice.Models;
using BankingApp.Controllers;
using BankingApp.Data;
using BankingApp.Models;
using BankingApp.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using PasswordGenerator;
using System.Data;
using System.Security.Principal;

namespace BankingApp.Areas.BackOffice.Controllers
{
	[Authorize]
	[HasPermission("Manage Customers")]
	[Area("BackOffice")]
	[Route("BackOffice/[controller]")]
	public class CustomersController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly ILogger<CustomersController> _logger;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IEmailSender _emailSender;
		private readonly IWebHostEnvironment _hostingEnvironment;
		private readonly IConfiguration _configuration;


		public CustomersController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, ILogger<CustomersController> logger, UserManager<ApplicationUser> userManager, IEmailSender emailSender, IWebHostEnvironment hostingEnvironment, IConfiguration configuration)
		{
			_context = context;
			_roleManager = roleManager;
			_logger = logger;
			_userManager = userManager;
			_emailSender = emailSender;
			_hostingEnvironment = hostingEnvironment;
			_configuration = configuration;
		}

		public async Task<IActionResult> Index(int activeTab = 1, string? custId = null, int accId = 0)
		{
			try
			{
				if (activeTab == 2 || activeTab == 4 || activeTab == 5 || activeTab == 6)
				{
					return View(new ManageCustomersViewModel()
					{
						Customer = await _userManager.FindByIdAsync(custId),
						Account = await _context.Accounts.Include(a => a.Customer).FirstOrDefaultAsync(a => a.Id == accId),
						ActiveTab = activeTab,
						Customers = await GetUsersInRole("Customer"),

					});
				}
				return View(new ManageCustomersViewModel()
				{
					Customers = await GetUsersInRole("Customer"),
					Customer = new ApplicationUser()
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

			foreach (var c in users)
			{
				c.Accounts = await _context.Accounts.Where(a => a.CustomerId == c.Id).ToListAsync();
				c.LoanBalance = await GetLoanBalanceForCustomerAsync(c);
			}
			return users;
		}

		private async Task<decimal> GetLoanBalanceForCustomerAsync(ApplicationUser c)
		{
			decimal total = 0;
			var loans = await _context
					.Loans
					.Include(l => l.LoanItems)
					.Include(l => l.Account)
					.Where(a => a.Customer == c).ToListAsync();

			foreach (var loan in loans)
			{
				var trans = await _context.Transactions.Where(t => t.Account == loan.Account).ToListAsync();
				loan.Account.Transactions = trans;

				var paidAmount = loan.Account.Transactions.Sum(t => t.Amount);
				if (paidAmount > 0)
				{

					var noOfInstallmentsPaid = paidAmount / loan.MonthlyPayment;
					if (noOfInstallmentsPaid > 1)
					{
						if (int.TryParse(noOfInstallmentsPaid.ToString(), out int value))
						{
							int count = Convert.ToInt32(noOfInstallmentsPaid);
							var loanItem = loan.LoanItems[count - 1];
							total += loanItem.EndingBalance;
						}
						else
						{
							var count = Convert.ToInt32(Math.Truncate(noOfInstallmentsPaid));
							var loanItem = loan.LoanItems[count - 1];
							var expectedPaid = loan.MonthlyPayment * count;
							var partyPaidAmount = paidAmount - expectedPaid;
							var outstanding = loanItem.EndingBalance - partyPaidAmount;
							total += outstanding;

						}
					}
					else
					{
						var outstanding = loan.BankOfferAmount - paidAmount;
						total += outstanding;
					}

				}
				else
				{
					total += loan.BankOfferAmount;
				}
			}

			return total;
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
					mUser.FirstName = model.Customer.FirstName;
					mUser.LastName = model.Customer.LastName;
					mUser.Email = model.Email;
					mUser.PhoneNumber = model.PhoneNumber;
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
					var pwd = new Password();
					var password = pwd.Next();
					model.Customer.UserName = model.Email;
					model.Customer.Id = Guid.NewGuid().ToString();
					model.Customer.DefaultPassword = password;
					model.Customer.Email = model.Email;
					model.Customer.PhoneNumber = model.PhoneNumber;

					var custId = await _context.Settings.FirstOrDefaultAsync(s => s.Key.Equals("Other_BaseCustomerId"));
					model.Customer.CustomerUniqueId = Convert.ToInt64(custId.Value) + 1;

					await _userManager.CreateAsync(model.Customer, password);
					await _userManager.AddToRoleAsync(model.Customer, "Customer");

					custId.Value = model.Customer.CustomerUniqueId.ToString();
					await _context.SaveChangesAsync();

					await SendEmailConfirmation(model.Customer);

				}

				return RedirectToAction("Index");
			}
			model.ActiveTab = 2;
			return View("Index", model);
		}


		[Route("LoadCustomerDetails")]
		public async Task<IActionResult> LoadCustomerDetails(string customerId)
		{
			try
			{
				var customer = await _context.ApplicationUsers
					.Include(c => c.Accounts)
					.FirstOrDefaultAsync(c => c.Id.Equals(customerId));

				var loans = await _context.Loans.Where(l => l.Customer == customer && l.LoanStatus == LoanStatus.Completed).ToListAsync();
				ViewBag.LoanTotal = loans.Sum(l => l.BankOfferAmount);

				return PartialView("_CustomerDetailsPartial", new ManageCustomersViewModel()
				{
					Customer = customer,
					Email = customer.Email,
					PhoneNumber = customer.PhoneNumber
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
				var loans = await _context
					.Loans
					.Include(l => l.LoanItems)
					.Include(l => l.Account)
					.Where(a => a.Customer == customer).ToListAsync();

				foreach (var loan in loans)
				{
					var trans = await _context.Transactions.Where(t => t.Account == loan.Account).ToListAsync();
					loan.Account.Transactions = trans;
				}

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
				var accNo = await _context.Settings.FirstOrDefaultAsync(s => s.Key.Equals("Account_BaseAccountNo"));
				var nextAccNo = Convert.ToInt64(accNo.Value) + 1;
				return PartialView("_CustomerCreateAccountPartial", new ManageCustomersViewModel()
				{
					Account = new Account()
					{
						Customer = customer,
						CreatedBy = currentUser.Id,
						AccountNo = nextAccNo
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
				if (model.Account.AccountNo <= 0)
				{
					ModelState.AddModelError("Empty Account No", "Account No cannot be empty.");
					return RedirectToAction("Index", new { activeTab = 6, custId = model.Account.Customer.Id });

				}

				var currentUser = await _userManager.GetUserAsync(HttpContext.User);

				var customer = await _userManager.FindByIdAsync(model.Account.Customer.Id);
				model.Account.Customer = customer;
				model.Account.CreatedBy = currentUser.Id;

				var accNo = await _context.Settings.FirstOrDefaultAsync(s => s.Key.Equals("Account_BaseAccountNo"));
				model.Account.AccountNo = Convert.ToInt64(accNo.Value) + 1;

				await _context.Accounts.AddAsync(model.Account);

				accNo.Value = model.Account.AccountNo.ToString();

				await _context.SaveChangesAsync();

				return RedirectToAction("Index", new { activeTab = 4, custId = customer.Id });

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
				var acc = await _context.Accounts.Include(a => a.Customer).FirstOrDefaultAsync(a => a.Id == accountId);
				var trans = await _context.Transactions
					.Include(t => t.CreatedBy)
					.Include(t => t.DestinationAccount)
					.Where(t => t.AccountId == accountId)
					.ToListAsync();

				return PartialView("_CustomerAccountTransactionsPartial", new ManageCustomersViewModel()
				{
					Transaction = new Transaction(),
					Transactions = trans,
					Account = acc
				});
			}
			catch (Exception)
			{
				throw;
			}
		}

		[Route("LoadAddNewCustomerView")]
		public async Task<IActionResult> LoadAddNewCustomerView()
		{
			try
			{
				var custId = await _context.Settings.FirstOrDefaultAsync(s => s.Key.Equals("Other_BaseCustomerId"));
				var nextCustId = Convert.ToInt64(custId.Value) + 1;

				return PartialView("_CustomerAddNewPartial", new ManageCustomersViewModel()
				{
					Customer = new ApplicationUser()
					{
						Id = string.Empty,
						CustomerUniqueId = nextCustId,
					}
				});
			}
			catch (Exception)
			{
				throw;
			}
		}

		[Route("LoadCustomerList")]
		public async Task<IActionResult> LoadCustomerList(string status)
		{
			try
			{
				var customers = await GetUsersInRole("Customer");

				switch (status)
				{
					case "active":
						customers = customers.Where(c => c.IsActive).ToList(); break;
					case "inactive":
						customers = customers.Where(c => !c.IsActive).ToList(); break;
					case "all":
						break;
					default:
						break;
				}

				return PartialView("_CustomerListPartial", new ManageCustomersViewModel()
				{
					Customers = customers
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

				var account = await _context.Accounts.Include(d => d.Customer).FirstOrDefaultAsync(a => a.Id == model.Account.Id);
				if (model.Transaction.Amount > 0)
				{
					var cu = await _userManager.GetUserAsync(HttpContext.User);
					model.Customer = await _userManager.FindByIdAsync(model.Account.Customer.Id);
					var desaccount = await _context.Accounts.Include(d => d.Customer).FirstOrDefaultAsync(a => a.AccountNo == model.DestinationAccountNo);
					model.Transaction.Account = account;
					model.Transaction.DestinationAccount = desaccount;
					model.Transaction.CreatedBy = cu;

					switch (model.Transaction.TransactionType)
					{
						case TransactionType.Deposit:
							account.Balance += model.Transaction.Amount;
							model.Transaction.DestinationAccount = account;
							model.Customer.AccountBalance += model.Transaction.Amount;
							break;
						case TransactionType.Withdrawal:
							account.Balance -= model.Transaction.Amount;
							model.Transaction.DestinationAccount = account;
							model.Transaction.Amount *= -1;
							model.Customer.AccountBalance -= model.Transaction.Amount;

							break;
						case TransactionType.Credit:
							account.Balance += model.Transaction.Amount;
							model.Transaction.DestinationAccount = account;
							break;
						case TransactionType.Debit:
							account.Balance -= model.Transaction.Amount;
							model.Transaction.DestinationAccount = account;
							model.Transaction.Amount *= -1;
							break;
						case TransactionType.TransferToLocal:
							account.Balance -= model.Transaction.Amount;
							desaccount.Balance += model.Transaction.Amount;
							model.Transaction.DestinationAccount = desaccount;
							model.Transaction.Description = "To: " + desaccount.Customer.FirstName + " " + desaccount.Customer.LastName + "-" + desaccount.AccountNo;
							model.Customer.AccountBalance -= model.Transaction.Amount;

							await _context.Transactions.AddAsync(new Transaction()
							{
								Account = desaccount,
								Amount = model.Transaction.Amount,
								DestinationAccount = desaccount,
								CreatedBy = cu,
								Description = "From: " + account.Customer.FirstName + " " + account.Customer.LastName + "-" + account.AccountNo,
								Reference = model.Transaction.Reference,
								Balance = desaccount.Balance,
								TransactionType = TransactionType.TransferToLocal
							});
							desaccount.Customer.AccountBalance += model.Transaction.Amount;
							model.Transaction.Amount *= -1;
							break;
						case TransactionType.TransferToLoan:
							account.Balance -= model.Transaction.Amount;
							desaccount.Balance -= model.Transaction.Amount;
							model.Transaction.DestinationAccount = desaccount;
							model.Transaction.Description = "To loan: " + desaccount.AccountNo;
							model.Customer.AccountBalance -= model.Transaction.Amount;

							await _context.Transactions.AddAsync(new Transaction()
							{
								Account = desaccount,
								Amount = model.Transaction.Amount,
								DestinationAccount = desaccount,
								CreatedBy = cu,
								Description = "From: " + account.Customer.FirstName + " " + account.Customer.LastName + "-" + account.AccountNo,
								Reference = model.Transaction.Reference,
								Balance = desaccount.Balance,
								TransactionType = TransactionType.TransferToLocal
							});
							//desaccount.Customer.LoanBalance -= model.Transaction.Amount;
							model.Transaction.Amount *= -1;
							break;
						default:
							break;
					}

					model.Transaction.Balance = account.Balance;

					await _context.Transactions.AddAsync(model.Transaction);
					await _context.SaveChangesAsync();
				}

				//var trans = await _context.Transactions.Where(a => a.AccountId == account.Id).ToListAsync();
				return RedirectToAction("LoadAccountTransactions", new { accountId = account.Id });


			}
			catch (Exception)
			{

				throw;
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

				await es.SendEmailAsync(user.Email, "SIF User Account Created", messageBody);

				return true;
			}
			catch (Exception ee)
			{
				_logger.LogError(ee, ee.Message);
			}
			return false;
		}

		[HttpGet("IsEmailInUser")]
		[HttpPost("IsEmailInUser")]
		[AllowAnonymous]
		public async Task<IActionResult> IsEmailInUser([Bind(Prefix = "Customer.Email")] string email)
		{
			var user = await _userManager.FindByNameAsync(email);
			return user == null ? Json(true) : Json($"Email {email} is already taken");
		}


		// Function to check for a valid address
		// Note: address variable parameter connects to data attribute in remote call 
		[HttpPost("IsValidAccount")]
		public async Task<IActionResult> IsValidAccount(long account)
		{
			if (account != 0)
			{
				var acc = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNo == account);
				if (acc == null)
				{
					return new JsonResult(false);
				}
			}
			return new JsonResult(true);
		}

		[HttpGet("DeativateAccount")]
		public async Task<IActionResult> DeativateAccount(int accId)
		{
			var acc = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == accId);
			acc.IsActive = !acc.IsActive;
			await _context.SaveChangesAsync();
			return Json(true);
		}

		[HttpGet("Accounts")]
		public ActionResult Accounts(string term, int type)
		{
			if (type == 5)
			{
				var routeList = _context.Accounts
					.Include(c => c.Customer)
					.Where(r => r.AccountNo.ToString().Contains(term) || r.Customer.FirstName.ToLower().Contains(term) || r.Customer.LastName.ToLower().Contains(term))
			   .Take(5)
			   .Select(r => new { id = r.AccountNo, label = r.Customer.FirstName + " " + r.Customer.LastName + " - " + r.AccountNo + " - " + r.AccountType.ToString(), name = "DestinationAccountNo" }).ToList();

				return Json(routeList);


			}
			else
			{
				var loans = _context.Loans
								  .Include(l => l.Customer)
								  .Include(l => l.Account)
									.Where(r => r.Account.AccountNo.ToString().Contains(term) || r.Customer.FirstName.ToLower().Contains(term) || r.Customer.LastName.ToLower().Contains(term))
									 .Take(5)
									 .Select(r => new { id = r.Account.AccountNo, label = r.Customer.FirstName + " " + r.Customer.LastName + " - " + r.Account.AccountNo + " - Loan", name = "DestinationAccountNo" }).ToList();

				return Json(loans);
			}
		}


	}
}
