using System.ComponentModel.DataAnnotations;

namespace BankingApp.Models
{
    public class Loan
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public ApplicationUser? Customer { get; set; }
        [Required]
        public string? Purpose { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public decimal BankOfferAmount { get; set; }
        public int Term { get; set; }
        public decimal InterestRate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal MonthlyPayment { get; set; }
        public DateTime BankResponseDate { get; set; }
        public ApplicationUser? Employee { get; set; }
        public LoanStatus LoanStatus { get; set; } = LoanStatus.RequestSent;
        public IEnumerable<LoanItem>? LoanItems { get; set; }
    }
}
