namespace BankingApp.Areas.BackOffice.Models
{
	public class DashboardViewModel
	{
		public int CustomerCount { get; set; }
		public decimal TotalLoansSum { get; set; }
		public decimal TotalDeposits { get; set; }
	}

	public class Chart
	{
		public decimal[]? Deposits { get; set; }
		public decimal[]? Withdrawals { get; set; }
		public string[]? Months { get; set; }
		public decimal[]? AccountTypeDeposits { get; set; }
	}
}
