using BankingApp.Areas.BackOffice.Models;
using BankingApp.Controllers;
using BankingApp.Data;
using BankingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PasswordGenerator;
using RestSharp;
using System.Data;

namespace BankingApp.Areas.BackOffice.Controllers
{
	[Authorize]
	[Area("BackOffice")]
	[Route("BackOffice/[controller]")]
	public class ExchangeController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly ILogger<ExchangeController> _logger;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IHttpClientFactory _clientFactory;

		public ExchangeController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, ILogger<ExchangeController> logger, UserManager<ApplicationUser> userManager, IHttpClientFactory client)
		{
			_context = context;
			_roleManager = roleManager;
			_logger = logger;
			_userManager = userManager;
			_clientFactory = client;
		}

		public async Task<IActionResult> Index()
		{
			try
			{
				return View(new ManageExchangesViewModel()
				{
					Exchanges = await _context.Exchanges.ToListAsync(),
					Exchange = new Exchange()

				});
			}
			catch (Exception e)
			{
				_logger.LogError(e.Message);
				return View();
			}
		}


		[HttpPost("AddUpdateExchange")]
		public async Task<IActionResult> AddUpdateExchange(ManageExchangesViewModel model)
		{
			if (ModelState.IsValid)
			{

				if (model.Exchange.Id > 0)
				{
					//model.Customer.UpdatedBy = currentUser;
					var exchange = await _context.Exchanges.FirstOrDefaultAsync(u => u.Id == model.Exchange.Id);
					exchange.Name = model.Exchange.Name;
					exchange.Spread = model.Exchange.Spread;
					exchange.SellingRate = model.Exchange.SellingRate;
					exchange.BuyingRate = model.Exchange.BuyingRate;
					exchange.MarketRate = model.Exchange.MarketRate;
					exchange.Symbol = model.Exchange.Symbol;
					await _context.SaveChangesAsync();
				}
				else
				{

					await _context.Exchanges.AddAsync(model.Exchange);
					await _context.SaveChangesAsync();
					var excha = await _context.Exchanges.FirstOrDefaultAsync(e => e.Id == model.Exchange.Id);
					excha.ExchangeId = excha.Id;
					await _context.SaveChangesAsync();
				}


				return RedirectToAction("Index");

			}

			return View("Index", model);
		}


		[Route("LoadExchnageDetails")]
		public async Task<IActionResult> LoadExchnageDetails(int exchnageId)
		{
			try
			{
				var exchanges = await _context.Exchanges.Where(b => b.Id == exchnageId).ToListAsync();
				return PartialView("_ExchangeDetailsPartial", new ManageExchangesViewModel()
				{
					Exchanges = exchanges,
					Exchange = exchanges.OrderByDescending(e => e.LastUpdatedDate).FirstOrDefault()
				}); ;
			}
			catch (Exception)
			{
				throw;
			}
		}

		[HttpPost("UpdateExchnageDetails")]
		public async Task<IActionResult> UpdateExchnageDetails(int exhangeId, decimal marketRate, decimal sRate, decimal bRate, decimal spread)
		{
			try
			{
				var ex = await _context.Exchanges.FirstOrDefaultAsync(f => f.Id == exhangeId);
				//var nEx = new Exchange()
				//{
				//	ExchangeId = exhangeId,
				//	Spread = spread,
				//	MarketRate = marketRate,
				//	SellingRate = sRate,
				//	BuyingRate = bRate,
				//	LastUpdatedDate = DateTime.Now,
				//	Name = ex.Name,
				//	ChangePercantage = ex.ChangePercantage,
				//	Symbol = ex.Symbol
				//};

				ex.Spread = spread;
				ex.MarketRate = marketRate;
				ex.SellingRate = sRate;
				ex.BuyingRate= bRate;

				//await _context.Exchanges.AddAsync(ex);
				await _context.SaveChangesAsync();
				return Json(true);
			}
			catch (Exception)
			{
				throw;
			}
		}
		[HttpGet("RefreshExchageRates")]
		public async Task<IActionResult> RefreshExchageRates()
		{
			try
			{
				var td = DateTime.Today.Year + "-" + DateTime.Today.Month.ToString().PadLeft(2,'0') + "-" + DateTime.Today.Day;
				var pd = DateTime.Today.AddDays(-1).Year + "-" + DateTime.Today.AddDays(-1).Month.ToString().PadLeft(2, '0') + "-" + DateTime.Today.AddDays(-1).Day;

				var request = new HttpRequestMessage(HttpMethod.Get, string.Format("https://api.apilayer.com/exchangerates_data/timeseries?start_date={0}&end_date={1}9&symbols=GBP%2CAUD&base=USD", pd, td));
				request.Headers.Add("Accept", "application/vnd.github.v3+json");

				var client = _clientFactory.CreateClient();
				client.DefaultRequestHeaders.Add("apikey", "knd0chhja3lUdrW3VVNtaJG33MF18c4N");

				var response = await client.GetAsync(request.RequestUri);
				var contents = response.Content.ReadAsStringAsync().Result;
				dynamic stuff = JObject.Parse(contents);

				var exchangeRates = new List<Exchange>();
				var selectedRate = new Exchange();


				var count = 1;
				foreach (var day in stuff.rates)
				{
					var existingRates = await _context.Exchanges.ToListAsync();
					foreach (var cur in day.First)
					{
						var curExists = existingRates.Exists(d => d.Symbol.Equals(cur.Name));
						if (curExists)
							selectedRate = existingRates.FirstOrDefault(d => d.Symbol.Equals(cur.Name));
						else
						{
							var nr = new Exchange()
							{
								Name = cur.Name,
								Symbol = cur.Name,
								MarketRatePreviousDay = cur.Value,
								LastUpdatedDate = DateTime.Now								
							};

							await _context.Exchanges.AddAsync(nr);
							await _context.SaveChangesAsync();
							continue;
						}
							
						if (count ==1)
							selectedRate.MarketRatePreviousDay = cur.Value;					
						else
							selectedRate.MarketRate = cur.Value;

					}

					count++;
				}

				await _context.SaveChangesAsync();

				return RedirectToAction("Index");
			}
			catch (Exception)
			{

				throw;
			}
		}

	}
}
