using BankingApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BankingApp.Areas.BackOffice.Models
{
	public class ManageEmployersViewModel
	{
		public List<ApplicationUser>? Employers { get; set; }
		public ApplicationUser? Employer { get; set; }
		public List<PermissionModel>? EPermissions { get; set; }
		public int ActiveTab { get; set; } = 1;
	}

	public class PermissionModel
	{
		public Permission? Permission { get; set; }
		public bool IsSelected { get; set; }
		public string? SelectedPermission { get; set; }
		public bool? IsSelectionActive => IsSelected;
	}

}