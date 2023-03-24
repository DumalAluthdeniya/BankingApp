using BankingApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BankingApp.Areas.BackOffice.Models
{
	public class ManageCustomersViewModel
	{
		public List<ApplicationUser>? Customers { get; set; }
		public ApplicationUser? Customer { get; set; }
		public IEnumerable<Account>? Accounts { get; set; }
		public Account? Account { get; set; }
		public Transaction? Transaction { get; set; }
		public List<Transaction>? Transactions { get; set; }
		public IEnumerable<Loan>? Loans { get; set; }
		public AccountType SelectedAccType { get; set; }
		public int ActiveTab { get; set; } = 1;
	}
}