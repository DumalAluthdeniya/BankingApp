using BankingApp.Models;

namespace BankingApp.Areas.BackOffice.Models
{
	public class ManageExchangesViewModel
	{
		public List<Exchange>? Exchanges { get; set; }
		public List<ExchangeHistory>? History { get; set; }
		public Exchange? Exchange { get; set; }
		public string? SelectedCurrency { get; set; }
		public string? BaseCurrency { get; set; }
		public string? User { get; set; }
	}

	public class ChartExchange
	{
		public decimal[] RatesMarket { get; set; }
		public decimal[] RatesSelling { get; set; }
		public decimal[] RatesBuying { get; set; }
		public string[] Months { get; set; }
	}
}