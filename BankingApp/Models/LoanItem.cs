namespace BankingApp.Models
{
	public class LoanItem
	{
		public int Id { get; set; }
		public Loan? Loan { get; set; }
		public decimal Capital { get; set; }
		public decimal Interest { get; set; }
		public decimal Amount { get; set; }
		public decimal Balance { get; set; }
	}
}
