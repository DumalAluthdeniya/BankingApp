using BankingApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BankingApp.Areas.BackOffice.Models
{
	public class ManageExchangesViewModel
	{
		public List<Exchange>? Exchanges { get; set; }
		public Exchange? Exchange { get; set; }
		public string? SelectedCurrency { get; set; }
	}


}