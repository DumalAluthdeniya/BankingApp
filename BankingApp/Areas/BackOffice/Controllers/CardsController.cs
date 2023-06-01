using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Areas.BackOffice.Controllers
{
	[Authorize]
	[HasPermission("Manage Cards")]
	[Area("BackOffice")]
	[Route("BackOffice/[controller]")]
	public class CardsController : Controller
	{
		


		public IActionResult Index()
		{
			try
			{
				return View();
			}
			catch (Exception e)
			{
				return View();
			}
		}

		
	}
}
