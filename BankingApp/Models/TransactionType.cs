using System.ComponentModel.DataAnnotations;

namespace BankingApp.Models
{
	public enum TransactionType
	{
		Deposit = 1,
		Withdrawal = 2,
		TransferToLocal = 3,
		TransferToLoan = 4,
		Interest = 5,
	}
}
