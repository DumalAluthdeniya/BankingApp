using BankingApp.Models;
using System.ComponentModel.DataAnnotations;

namespace BankingApp.Areas.BackOffice.Models
{
	public class ManageCustomersViewModel
	{
		public List<ApplicationUser>? Customers { get; set; }
		public ApplicationUser? Customer { get; set; }
		[Required]
		[EmailAddress]
		public string? Email { get; set; }
		[Required]
		public string? PhoneNumber { get; set; }
		public IEnumerable<Account>? Accounts { get; set; }
		public Account? Account { get; set; }
		public Transaction? Transaction { get; set; }
		public List<Transaction>? Transactions { get; set; }
		public IEnumerable<Loan>? Loans { get; set; }
		public AccountType SelectedAccType { get; set; }
		public int ActiveTab { get; set; } = 1;
	}
}