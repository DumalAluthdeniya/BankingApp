using System.ComponentModel.DataAnnotations;

namespace BankingApp.Models
{
	public enum AccountType
	{
		[Display(Name = "Savings Account")]
		Savings = 1,
		[Display(Name = "Spending Account")]
		Spending = 2,
		[Display(Name = "Loan Account")]
		Loan = 3
	}
}
