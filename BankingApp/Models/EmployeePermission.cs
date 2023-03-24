using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BankingApp.Models
{
    public class EmployeePermission
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public ApplicationUser? Employee { get; set; }
        [Required]
        public Permission? Permission { get; set; }
        [Required]
        public bool IsActive { get; set; } = false;
		[Required]
        public PermissionType PermissionType { get; set; }
    }
}
