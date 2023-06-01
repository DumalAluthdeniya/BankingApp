using System.ComponentModel.DataAnnotations;

namespace BankingApp.Models
{
	public class Branch
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Phone { get; set; }
		[Required]
		public string? Email { get; set; }       
        public string? AddressLine1 { get; set; } = string.Empty;
        public string? AddressLine2 { get; set; }       
        public string? City { get; set; }     
        public string? Zip { get; set; }     
        public string? Region { get; set; }     
        public string? Country { get; set; }
        public ApplicationUser? Manager { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public ApplicationUser? CreatedBy { get; set; }
        public DateTime UpdatedTime { get; set; }
        public ApplicationUser? UpdatedBy { get; set; }


    }
}
