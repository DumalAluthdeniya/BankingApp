using System.ComponentModel.DataAnnotations;

namespace BankingApp.Models
{
	public enum AccountType
	{
		[Display(Name = "Savings Account")]
		Savings = 1,
		[Display(Name = "Current Account")]
		Current = 2
	}
}
