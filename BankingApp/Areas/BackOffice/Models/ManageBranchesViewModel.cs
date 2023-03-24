using BankingApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BankingApp.Areas.BackOffice.Models
{
	public class ManageBranchesViewModel
	{
		public List<Branch>? Branches { get; set; }
		public Branch? Branch { get; set; }
		public List<SelectListItem>? Managers { get; set; }
		public string? SelectedManager { get; set; }
		public int ActiveTab { get; set; } = 1;
	}
}