using BankingApp.Models;
using System.ComponentModel.DataAnnotations;

namespace BankingApp.Areas.BackOffice.Models
{
	public class ManageEmployersViewModel
	{
		public List<ApplicationUser>? Employers { get; set; }
		public ApplicationUser? Employer { get; set; }
		public List<PermissionModel>? EPermissions { get; set; }
		public int ActiveTab { get; set; } = 1;
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		public string? Password { get; set; }
		[Required]
		[EmailAddress]
		public string? Email { get; set; }
		[Required]
		public string? PhoneNumber { get; set; }
	}

	public class PermissionModel
	{
		public Permission? Permission { get; set; }
		public bool IsSelected { get; set; }
		public string? SelectedPermission { get; set; }
		public bool? IsSelectionActive => IsSelected;
	}

}