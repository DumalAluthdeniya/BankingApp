using BankingApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BankingApp.Areas.BackOffice.Models
{
	public class SupportsViewModel
	{
		public List<Support>? Supports { get; set; }
		public Support? Support { get; set; }
		public int ActiveTab { get; set; }
		public List<SelectListItem>? Customers { get; set; }
		public string? SelectedCustomer { get; set; }
		public string? Message { get; set; }
		public List<IFormFile>? Files { get; set; }
	}
}
