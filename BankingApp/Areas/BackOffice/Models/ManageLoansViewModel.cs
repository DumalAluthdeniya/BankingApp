using BankingApp.Models;

namespace BankingApp.Areas.BackOffice.Models
{
	public class ManageLoansViewModel
	{
		public List<Loan>? Loans { get; set; }
		public Loan? Loan { get; set; }
		public int ActiveTab { get; set; } = 1;
	}
}