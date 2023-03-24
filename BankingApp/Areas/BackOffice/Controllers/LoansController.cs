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

		public async Task<IActionResult> Index()
		{
			try
			{
				return View(new ManageLoansViewModel()
				{
					Loans = await _context.Loans.Include(l => l.Customer).ToListAsync(),
					ActiveTab = 1
				});
			}
			catch (Exception e)
			{
				_logger.LogError(e.Message);
				return View();
			}
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

		[HttpGet("LoadOpenLoanDetails")]
		public async Task<IActionResult> LoadOpenLoanDetails(int loanId)
		{
			try
			{
				return PartialView("_OpenLoanDetailsPartial", new ManageLoansViewModel()
				{
					Loan = await _context.Loans
					.Include(l => l.Customer)
					.Include(l => l.Employee)
					.Include(l => l.LoanItems)
					.SingleAsync(l => l.Id == loanId),
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
				loan.BankResponseDate= DateTime.Now;
				loan.BankOfferAmount = model.Loan.BankOfferAmount;
				loan.Term = model.Loan.Term;
				loan.InterestRate = model.Loan.InterestRate;
				loan.TotalAmount = model.Loan.TotalAmount;
				loan.MonthlyPayment = model.Loan.MonthlyPayment;
				loan.Employee = await _userManager.GetUserAsync(HttpContext.User);

				await _context.SaveChangesAsync();

				model.ActiveTab = 4;
				model.Loans = await _context.Loans.Include(l => l.Customer).ToListAsync();

				return View("Index", model);
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
				var loan = await _context.Loans.FirstOrDefaultAsync(l => l.Id == model.Loan.Id);
				loan.LoanStatus = LoanStatus.Completed;
				loan.BankResponseDate = DateTime.Now;
				loan.BankOfferAmount = model.Loan.BankOfferAmount;
				loan.Term = model.Loan.Term;
				loan.InterestRate = model.Loan.InterestRate;
				loan.TotalAmount = model.Loan.TotalAmount;
				loan.MonthlyPayment = model.Loan.MonthlyPayment;
				loan.Employee = await _userManager.GetUserAsync(HttpContext.User);

				var loanItems = new List<LoanItem>();

				for (int i = 1; i < loan.Term; i++)
				{
					loanItems.Add(new LoanItem()
					{
						Capital = loan.BankOfferAmount,
						Amount = loan.MonthlyPayment,
						Interest = loan.InterestRate,
						Loan = loan
					});
				}

				loan.LoanItems= loanItems;

				await _context.SaveChangesAsync();

				model.ActiveTab = 3;
				model.Loans = await _context.Loans.Include(l => l.Customer).ToListAsync();

				return View("Index", model);
			}
			catch (Exception)
			{

				throw;
			}
		}


	}
}
