using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingApp.Models
{
	public class Account
	{
		[Key]
		public int Id { get; set; }
		public AccountType AccountType { get; set; }
		public string CustomerId { get; set; }
		public ApplicationUser? Customer { get; set; }
		[Required]
		public long AccountNo { get; set; }

		[Column(TypeName = "decimal(10, 2)")]
		public decimal Balance { get; set; }
		public DateTime CreatedDate { get; set; } = DateTime.Now;
		public string? CreatedBy { get; set; }
		public bool IsActive { get; set; } = true;
		[NotMapped]
		public IEnumerable<Transaction>? Transactions { get; set; }
	}
}
