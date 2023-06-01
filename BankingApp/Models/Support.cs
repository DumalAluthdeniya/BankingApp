using System.ComponentModel.DataAnnotations;

namespace BankingApp.Models
{
    public class Support
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Ticket { get; set; }
        [Required]
        public ApplicationUser? Customer { get; set; }
        [Required]
        public string? Subject { get; set; }
		[Required]
		public string? Area { get; set; }
        public bool IsActive { get; set; } = true;
		public DateTime CreatedDate { get; set;} = DateTime.Now;
		public ApplicationUser? CreatedBy { get; set;}

    }
}
