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
using Org.BouncyCastle.Utilities;
using PasswordGenerator;
using Sentry;
using System;
using System.Data;

namespace BankingApp.Areas.BackOffice.Controllers
{
	[Authorize]
	[HasPermission("Manage Loan")]
	[Area("BackOffice")]
	[Route("BackOffice/[controller]")]
	public class LoansController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly ILogger<ExchangeController> _logger;
		private readonly UserManager<ApplicationUser> _userManager;

		public LoansController(ApplicationDbContext context, ILogger<ExchangeController> logger, UserManager<ApplicationUser> userManager)
		{
			_context = context;
			_logger = logger;
			_userManager = userManager;
		}

		public async Task<IActionResult> Index(int activeTab = 2, int loanId = 0, bool fromAcc = false, long accNo = 0)
		{
			try
			{
				if (fromAcc)
				{
					var lAcc = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNo == accNo);
					var loan = await _context.Loans.FirstOrDefaultAsync(l => l.Account == lAcc);
					return View(new ManageLoansViewModel()
					{
						LoanId = loan.Id,
						Loans = await _context.Loans.Include(l => l.Account).Include(l => l.Customer).ToListAsync(),
						Loan = loan,
						ActiveTab = 3
					});
				}
				return View(new ManageLoansViewModel()
				{
					LoanId = loanId,
					Loans = await _context.Loans.Include(l => l.Account).Include(l => l.Customer).ToListAsync(),
					ActiveTab = activeTab,
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


		[HttpGet("LoadLoanDetails")]
		public async Task<IActionResult> LoadLoanDetails(int loanId)
		{
			try
			{
				return PartialView("_LoanRequestDetailsPartial", new ManageLoansViewModel()
				{
					Loan = await _context.Loans.Include(l => l.Customer).Include(l => l.Employee).SingleAsync(l => l.Id == loanId),
				});
			}
			catch (Exception)
			{
				throw;
			}
		}

		[HttpGet("LoadNewLoanView")]
		public async Task<IActionResult> LoadNewLoanView()
		{
			try
			{
				return PartialView("_OpenNewLoanPartial", new ManageLoansViewModel()
				{
					Loan = new Loan(),
				});
			}
			catch (Exception)
			{
				throw;
			}
		}

		[HttpGet("LoadOpenLoanDetails")]
		public async Task<IActionResult> LoadOpenLoanDetails(int loanId)
		{
			try
			{
				var loan = await _context.Loans
					.Include(l => l.Account)
					.Include(l => l.Customer)
					.Include(l => l.Employee)
					.Include(l => l.LoanItems)
					.SingleAsync(l => l.Id == loanId);

				var trans = await _context.Transactions.Where(t => t.Account == loan.Account).ToListAsync();

				loan.Account.Transactions = trans;

				return PartialView("_OpenLoanDetailsPartial", new ManageLoansViewModel()
				{
					Loan = loan
				});
			}
			catch (Exception)
			{
				throw;
			}
		}

		[HttpGet("LoadActiveLoans")]
		public async Task<IActionResult> LoadActiveLoans()
		{
			try
			{

				return PartialView("_ActiveLoansPartial", new ManageLoansViewModel()
				{
					Loans = await _context.Loans.Include(l => l.Customer).ToListAsync(),
				});
			}
			catch (Exception)
			{
				throw;
			}
		}

		[HttpGet("LoadNewLoan")]
		public async Task<IActionResult> LoadNewLoanAsync()
		{
			try
			{
				var interest = await _context.AccountFeeSettings.FirstOrDefaultAsync(c => c.Key.Equals("Loans_Accounts_InterestRate"));
				var accNo = await _context.Settings.FirstOrDefaultAsync(s => s.Key.Equals("Account_BaseAccountNo"));
				var nextAccNo = Convert.ToInt64(accNo.Value) + 1;
				return PartialView("_OpenNewLoanPartial", new ManageLoansViewModel()
				{
					Loan = new Loan()
					{
						InterestRate = interest != null ? Convert.ToDecimal(interest.Value) : 0,
						Account = new Account()
						{
							AccountNo = nextAccNo
						}
					},
					Customers = await GetCustomersSelectListItems(string.Empty)

				});
			}
			catch (Exception)
			{
				throw;
			}
		}

		[HttpPost("SendBankOffer")]
		public async Task<IActionResult> SendBankOffer(ManageLoansViewModel model)
		{
			try
			{
				var loan = await _context.Loans.FirstOrDefaultAsync(l => l.Id == model.Loan.Id);
				loan.LoanStatus = LoanStatus.Processing;
				loan.BankResponseDate = DateTime.Now;
				loan.BankOfferAmount = model.Loan.BankOfferAmount;
				loan.Term = model.Loan.Term;
				loan.InterestRate = model.Loan.InterestRate;
				loan.TotalAmount = model.Loan.TotalAmount;
				loan.MonthlyPayment = model.Loan.MonthlyPayment;
				loan.Employee = await _userManager.GetUserAsync(HttpContext.User);

				await _context.SaveChangesAsync();

				return RedirectToAction("Index", new { activeTab = 4, loanId = loan.Id });
			}
			catch (Exception)
			{

				throw;
			}
		}
		[HttpPost("OpenLoan")]
		public async Task<IActionResult> OpenLoan(ManageLoansViewModel model)
		{
			try
			{
				if (model.Loan.Id > 0)
				{
					var loan = await _context.Loans.FirstOrDefaultAsync(l => l.Id == model.Loan.Id);
					loan.LoanStatus = LoanStatus.Completed;
					loan.BankResponseDate = DateTime.Now;
					loan.BankOfferAmount = model.Loan.BankOfferAmount;
					loan.Term = model.Loan.Term;
					loan.InterestRate = model.Loan.InterestRate;
					loan.TotalAmount = model.Loan.TotalAmount;
					loan.MonthlyPayment = model.Loan.MonthlyPayment;
					loan.Employee = await _userManager.GetUserAsync(HttpContext.User);


					loan.LoanItems = GetLoanItems((model.Loan.Amount + model.Loan.Fees), model.Loan.Term, model.Loan.MonthlyPayment, model.Loan.InterestRate);
					await _context.SaveChangesAsync();

				}
				else
				{
					model.Loan.Amount = model.Loan.BankOfferAmount;
					model.Loan.LoanStatus = LoanStatus.Completed;
					model.Loan.Employee = await _userManager.GetUserAsync(HttpContext.User);
					model.Loan.BankResponseDate = DateTime.Now;
					model.Loan.CreatedDate = DateTime.Now;
					model.Loan.Customer = await _context.ApplicationUsers.FirstOrDefaultAsync(c => c.Id.Equals(model.SelectedCustomer));

					model.Loan.LoanItems = GetLoanItems((model.Loan.Amount + model.Loan.Fees), model.Loan.Term, model.Loan.MonthlyPayment, model.Loan.InterestRate);

					var accNo = await _context.Settings.FirstOrDefaultAsync(s => s.Key.Equals("Account_BaseAccountNo"));
					var nextAccNo = Convert.ToInt64(accNo.Value) + 1;

					var loanAccount = new Account()
					{
						AccountNo = nextAccNo,
						AccountType = AccountType.Loan,
						Customer = model.Loan.Customer,
						Balance = model.Loan.TotalAmount
					};

					model.Loan.Account = loanAccount;

					await _context.Loans.AddAsync(model.Loan);

					var accNonext = await _context.Settings.FirstOrDefaultAsync(s => s.Key.Equals("Account_BaseAccountNo"));
					accNonext.Value = model.Loan.Account.AccountNo.ToString();
					model.Loan.Customer.LoanBalance += model.Loan.TotalAmount;
					await _context.SaveChangesAsync();

				}


				return RedirectToAction("Index");
			}
			catch (Exception)
			{

				throw;
			}
		}

		[HttpPost("SettleLoanItem")]
		public async Task<IActionResult> SettleLoanItemAsync(int loanItemId)
		{
			try
			{
				var item = await _context.LoanItems.FirstOrDefaultAsync(i => i.Id == loanItemId);
				item.Outstanding = 0;
				await _context.SaveChangesAsync();

				return Json(true);

			}
			catch (Exception)
			{

				throw;
			}
		}

		[HttpGet("LoadLoanItemsPartial")]
		public async Task<IActionResult> LoadLoanItemsPartial(decimal amount, int term, decimal monthlyAmount, decimal interest)
		{
			try
			{
				List<LoanItem> items = GetLoanItems(amount, term, monthlyAmount, interest);

				return PartialView("_InstallmentPlanPartial", items);
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		private static List<LoanItem> GetLoanItems(decimal amount, int term, decimal monthlyAmount, decimal interest)
		{
			decimal equity = 0;
			decimal beginingBalance = amount;
			var mInterest = interest / 100 / 12;

			var items = new List<LoanItem>();

			for (int i = 0; i < term; i++)
			{
				var thisMonthsInterest = (amount - equity) * mInterest;
				equity += (monthlyAmount - thisMonthsInterest);
				decimal principle = monthlyAmount - thisMonthsInterest;
				decimal endingBalance = beginingBalance - principle;

				var li = new LoanItem()
				{
					BeginningBalance = beginingBalance,
					Interest = thisMonthsInterest,
					Principle = principle,
					EndingBalance = endingBalance < 1 ? 0 : endingBalance,
					Outstanding = endingBalance
				};

				beginingBalance = endingBalance;
				items.Add(li);
			}

			return items;
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



	}
}
