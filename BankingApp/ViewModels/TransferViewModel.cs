using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;

namespace BankingApp.ViewModels
{
	public class TransferViewModel
	{
		public List<SelectListItem>? Accounts { get; set; }
		public int SelectedSendAccount { get; set; }
		public int SelectedRequestAccount { get; set; }
		[Required]
		public decimal Amount { get; set; }
		public DateTime TransactionDate { get; set; } = DateTime.Now;
	}
}
