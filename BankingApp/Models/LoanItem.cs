using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingApp.Models
{
	public class LoanItem
	{
		public int Id { get; set; }
		
		public Loan? Loan { get; set; }
		
		[Column(TypeName = "decimal(10, 2)")]
		public decimal BeginningBalance { get; set; }

		[Column(TypeName = "decimal(10, 2)")]
		public decimal Interest { get; set; }

		[Column(TypeName = "decimal(10, 2)")]
		public decimal Principle { get; set; }

		[Column(TypeName = "decimal(10, 2)")]
		public decimal EndingBalance { get; set; }

		[Column(TypeName = "decimal(10, 2)")]
		public decimal Outstanding { get; set; }
	}
}
