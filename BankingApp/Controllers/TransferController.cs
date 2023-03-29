using BankingApp.Data;
using BankingApp.Models;
using BankingApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Controllers
{
	public class TransferController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
		public TransferController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		public async Task<IActionResult> Index()
		{
			var model = new TransferViewModel()
			{
				Accounts = await GetAccountsSelectListAsync()
			};
			return View(model);
		}

		private async Task<List<SelectListItem>> GetAccountsSelectListAsync(int id = 0)
		{
			var currentUser = await _userManager.GetUserAsync(HttpContext.User);

			var accounts = await _context.Accounts.Where(a => a.Customer == currentUser).ToListAsync();
			var accountsItems = new List<SelectListItem>();
			foreach (var a in accounts)
			{
				accountsItems.Add(new SelectListItem() { Text = (a.AccountType.ToString() + " " + a.AccountNo), Value = a.Id.ToString(), Selected = id == a.Id });
			}

			return accountsItems;
		}

		[HttpPost]
		public async Task<IActionResult> Index(TransferViewModel model)
		{
			if (model.Amount <= 0)
			{
				ModelState.AddModelError("Amount Required", "Amount is required.");
				model.Accounts = await GetAccountsSelectListAsync();
				return View(model);
			}

			var selectionSendAcc = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == model.SelectedSendAccount);
			var selectionReqAcc = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == model.SelectedRequestAccount);

			if(selectionSendAcc.Balance <= 0 || model.Amount > selectionSendAcc.Balance)
			{
				ModelState.AddModelError("Insuffuciat Balance", "Insufficient Balance");
				model.Accounts = await GetAccountsSelectListAsync();
				return View(model);
			}

			selectionSendAcc.Balance -= model.Amount;
			selectionReqAcc.Balance += model.Amount;

			var sendTra = new Transaction()
			{
				Account = selectionSendAcc,
				Amount = model.Amount,
				CreatedBy = await _userManager.GetUserAsync(HttpContext.User),
				DestinationAccount = selectionReqAcc,
				Description = @"From {selectionSendAcc.AccountNo} to {selectionReqAcc.AccountNo}",
				TransactionType = TransactionType.TransferToLocal,
				IsActive = true,
			};

			var reqTra = new Transaction()
			{
				Account = selectionReqAcc,
				Amount = model.Amount,
				CreatedBy = await _userManager.GetUserAsync(HttpContext.User),
				DestinationAccount = selectionReqAcc,
				Description = @"From {selectionSendAcc.AccountNo} to {selectionReqAcc.AccountNo}",
				TransactionType = TransactionType.TransferToLocal,
				IsActive = true,
			};
			await _context.AddAsync(sendTra);
			await _context.AddAsync(reqTra);
			await _context.SaveChangesAsync();

			return RedirectToAction("Index");
		}
	}
}
