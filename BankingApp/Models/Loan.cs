using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingApp.Models
{
    public class Loan
    {
        [Key]
        public int Id { get; set; }
        public Account Account { get; set; }
		public string CustomerId { get; set; }
		public ApplicationUser? Customer { get; set; }
        [Required]
        public string? Purpose { get; set; }
		public string? Collateral { get; set; }
		[Required]
		[Column(TypeName = "decimal(10, 2)")]
		public decimal Amount { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
		[Column(TypeName = "decimal(10, 2)")]
		public decimal BankOfferAmount { get; set; }
        public int Term { get; set; }
		[Column(TypeName = "decimal(10, 2)")]
		public decimal InterestRate { get; set; }
		[Column(TypeName = "decimal(10, 2)")]
		public decimal Fees { get; set; }
        public LoanType LoanType { get; set; }
		[Column(TypeName = "decimal(10, 2)")]
		public decimal TotalAmount { get; set; }
		[Column(TypeName = "decimal(10, 2)")]
		public decimal MonthlyPayment { get; set; }
        public DateTime BankResponseDate { get; set; }
        public ApplicationUser? Employee { get; set; }
        public LoanStatus LoanStatus { get; set; } = LoanStatus.RequestSent;
        public List<LoanItem>? LoanItems { get; set; }
    }
}
