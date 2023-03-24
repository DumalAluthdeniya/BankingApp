
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingApp.Models
{
	public class Transaction
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public TransactionType TransactionType { get; set; }
		public int AccountId { get; set; }
		public Account? Account { get; set; }
		[Required]
		[Column(TypeName = "decimal(10, 2)")]
		public decimal Amount { get; set; }
		public string? Description { get; set; }
		public string? Reference { get; set; }
		public int DestinationAccountId { get; set; }
		[ForeignKey("DestinationAccountId")]
		public Account? DestinationAccount { get; set; }
		public DateTime CreatedDate { get; set; } = DateTime.Now;
		public ApplicationUser? CreatedBy { get; set; }
		public bool IsActive { get; set; }



	}
}
