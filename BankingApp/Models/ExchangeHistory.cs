using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingApp.Models
{
	public class ExchangeHistory
	{
		[Key]
		public int Id { get; set; }
		public Exchange Exchange { get; set; }
		public int ExchangeId { get; set; }
		[Column(TypeName = "decimal(10, 2)")]
		public decimal MarketRate { get; set; }
		[Column(TypeName = "decimal(10, 2)")]
		public decimal SellingRate { get; set; }
		[Column(TypeName = "decimal(10, 2)")]
		public decimal BuyingRate { get; set; }
		public DateTime UpdatedDate { get; set; } = DateTime.Now;
		public ApplicationUser? UpdatedBy { get; set; }

	}
}
