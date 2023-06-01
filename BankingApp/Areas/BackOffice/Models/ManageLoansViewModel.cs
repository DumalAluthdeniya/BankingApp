using BankingApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BankingApp.Areas.BackOffice.Models
{
	public class ManageLoansViewModel
	{
		public List<Loan>? Loans { get; set; }
		public List<SelectListItem>? Customers { get; set; }
		public string? SelectedCustomer { get; set; }
		public Loan? Loan { get; set; }
		public int LoanId { get; set; }
		public int ActiveTab { get; set; } = 1;

	}
}