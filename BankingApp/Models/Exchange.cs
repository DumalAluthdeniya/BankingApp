using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingApp.Models
{
    public class Exchange
    {
        [Key]
        public int Id { get; set; }
		public int ExchangeId { get; set; }
		[Required]
        public string? Name { get; set; }
		[Required]
		public string? Symbol { get; set; }
        [Required]
        public decimal MarketRate { get; set; }
        public decimal MarketRatePreviousDay { get; set; }
		[Required]
        public decimal SellingRate { get; set; }
		[Required]
		public decimal BuyingRate { get; set; }
		[Required]
        public decimal Spread { get; set; }
        public decimal ChangePercantage { get; set; }
        [Required]
        public DateTime LastUpdatedDate { get; set; } = DateTime.Now;

    }
}
