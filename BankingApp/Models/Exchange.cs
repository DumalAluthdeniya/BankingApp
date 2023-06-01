using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingApp.Models
{
	public class Exchange
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string? Name { get; set; }
		[Required]
		public string? Symbol { get; set; }

		[Required]
		[Column(TypeName = "decimal(10, 2)")]
		public decimal MarketRate { get; set; }

		[Column(TypeName = "decimal(10, 2)")]
		public decimal MarketRatePreviousDay { get; set; }

		[Required]
		[Column(TypeName = "decimal(10, 2)")]
		public decimal SellingRate { get; set; }

		[Required]
		[Column(TypeName = "decimal(10, 2)")]
		public decimal BuyingRate { get; set; }

		[Required]
		[Column(TypeName = "decimal(10, 2)")]
		public decimal Spread { get; set; }

		[Column(TypeName = "decimal(10, 2)")]
		public decimal ChangePercantage { get; set; }

		[Required]
		public DateTime LastUpdatedDate { get; set; } = DateTime.Now;

	}
}
