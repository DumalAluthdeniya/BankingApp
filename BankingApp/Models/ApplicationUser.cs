using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string? FirstName { get; set; } = string.Empty;
        [Required]
        public string? LastName { get; set; } = string.Empty;
        [Required]
        public string? AddressLine1 { get; set; } = string.Empty;
        public string? AddressLine2 { get; set; } = string.Empty;
        [Required]
        public string? City { get; set; } = string.Empty;
        [Required]
        public string? Zip { get; set; } = string.Empty;
        [Required]
        public string? Region { get; set; } = string.Empty;
        [Required]
        public string? Country { get; set; } = string.Empty;
        public string? ProfileImage { get; set; } = string.Empty;
        [Required]
        public decimal LoanBalance { get; set; } = decimal.Zero;
        [Required]
        public decimal AccountBalance { get; set; } = decimal.Zero;
        public bool IsActive { get; set; } = true;
        public string? DefaultPassword { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
		public DateTime UpdatedDate { get; set; }
		public virtual IEnumerable<EmployeePermission>? EmployeePermissions { get; set; }
		public virtual IEnumerable<Account>? Accounts { get; set; }
    }
}
