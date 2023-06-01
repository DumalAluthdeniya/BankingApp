
using System.ComponentModel.DataAnnotations;

namespace BankingApp.Models
{
	public class SupportFile
	{
		[Key]
		public int Id { get; set; }
		public int SupportItemId { get; set; }
		public SupportItem? SupportItem { get; set; }
		public string? Path { get; set; }
		public DateTime UploadedDate { get; set; } = DateTime.Now;
	}
}
