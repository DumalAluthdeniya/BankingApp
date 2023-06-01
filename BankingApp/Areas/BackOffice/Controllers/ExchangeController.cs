using BankingApp.Areas.BackOffice.Models;
using BankingApp.Controllers;
using BankingApp.Data;
using BankingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.CodeAnalysis.Text;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Configuration;
using PasswordGenerator;
using RestSharp;
using System.Data;
using System.Linq;
using System.Runtime.ConstrainedExecution;

namespace BankingApp.Areas.BackOffice.Controllers
{
	[Authorize]
	[HasPermission("Manage Exchange")]
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
				var setting = await _context.Settings.FirstOrDefaultAsync(c => c.Key.Equals("Exchange_BaseCurrency"));
				var exs = await _context.Exchanges.ToListAsync();
				foreach (var item in exs)
				{
					var pdv = await _context.ExchangeHistory.Where(d => d.UpdatedDate.Date == DateTime.Now.AddDays(-1).Date).FirstOrDefaultAsync(d => d.ExchangeId == item.Id);
					item.MarketRatePreviousDay = pdv != null ? pdv.MarketRate : 0;
				}
				return View(new ManageExchangesViewModel()
				{
					Exchanges = exs,
					Exchange = new Exchange(),
					BaseCurrency = setting.Value

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
				var currentUser = await _userManager.GetUserAsync(HttpContext.User);

				if (model.Exchange.Id > 0)
				{
					var exchange = await _context.Exchanges.FirstOrDefaultAsync(u => u.Id == model.Exchange.Id);
					exchange.Name = model.Exchange.Name;
					exchange.Spread = model.Exchange.Spread;
					exchange.SellingRate = model.Exchange.SellingRate;
					exchange.BuyingRate = model.Exchange.BuyingRate;
					exchange.MarketRate = model.Exchange.MarketRate;
					exchange.Symbol = model.Exchange.Symbol;

					if (exchange.MarketRate != model.Exchange.MarketRate || exchange.SellingRate != model.Exchange.SellingRate || exchange.BuyingRate != model.Exchange.BuyingRate)
					{

						var exHis = new ExchangeHistory()
						{
							ExchangeId = exchange.Id,
							MarketRate = model.Exchange.MarketRate,
							BuyingRate = model.Exchange.BuyingRate,
							SellingRate = model.Exchange.SellingRate,
							UpdatedBy = currentUser
						};

						await _context.ExchangeHistory.AddAsync(exHis);
					}
					await _context.SaveChangesAsync();
				}
				else
				{

					await _context.Exchanges.AddAsync(model.Exchange);
					await _context.SaveChangesAsync();

					var exHis = new ExchangeHistory()
					{
						ExchangeId = model.Exchange.Id,
						MarketRate = model.Exchange.MarketRate,
						BuyingRate = model.Exchange.BuyingRate,
						SellingRate = model.Exchange.SellingRate,
						UpdatedBy = currentUser
					};

					await _context.ExchangeHistory.AddAsync(exHis);
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
				var currentUser = await _userManager.GetUserAsync(HttpContext.User);
				var exchanges = await _context.Exchanges.Where(b => b.Id == exchnageId).ToListAsync();
				var history = await _context.ExchangeHistory.Include(h => h.UpdatedBy).Where(b => b.ExchangeId == exchnageId).ToListAsync();
				var setting = await _context.Settings.FirstOrDefaultAsync(c => c.Key.Equals("Exchange_BaseCurrency"));
				return PartialView("_ExchangeDetailsPartial", new ManageExchangesViewModel()
				{
					Exchanges = exchanges,
					Exchange = exchanges.OrderByDescending(e => e.LastUpdatedDate).FirstOrDefault(),
					BaseCurrency = setting.Value,
					History = history,
					User = currentUser.FirstName + " " + currentUser.LastName

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
				var currentUser = await _userManager.GetUserAsync(HttpContext.User);

				var ex = await _context.Exchanges.FirstOrDefaultAsync(f => f.Id == exhangeId);

				if (ex.MarketRate != marketRate || ex.SellingRate != sRate || ex.BuyingRate != bRate)
				{
					var exHis = new ExchangeHistory()
					{
						ExchangeId = ex.Id,
						MarketRate = marketRate,
						BuyingRate = bRate,
						SellingRate = sRate,
						UpdatedBy = currentUser
					};

					await _context.ExchangeHistory.AddAsync(exHis);
				}

				ex.Spread = spread;
				ex.MarketRate = marketRate;
				ex.SellingRate = sRate;
				ex.BuyingRate = bRate;
				ex.LastUpdatedDate = DateTime.Now;

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
				var td = DateTime.Today.Year + "-" + DateTime.Today.Month.ToString("00") + "-" + DateTime.Today.Day.ToString("00");
				var pd = DateTime.Today.AddDays(-1).Year + "-" + DateTime.Today.AddDays(-1).Month.ToString("00") + "-" + DateTime.Today.AddDays(-1).Day.ToString("00");

				var settings = await _context.Settings.ToListAsync();
				var currencies = settings.Single(c => c.Key.Equals("Exchange_CurrencyList")).Value.Replace(",", "%2C");
				var baseCurrency = settings.Single(c => c.Key.Equals("Exchange_BaseCurrency")).Value;
				var apiKey = settings.Single(c => c.Key.Equals("Exchange_ApiLayerApiKey")).Value;

				var request = new HttpRequestMessage(HttpMethod.Get, string.Format("https://api.apilayer.com/exchangerates_data/timeseries?start_date={0}&end_date={1}&symbols={2}&base={3}", pd, td, currencies, baseCurrency));
				request.Headers.Add("Accept", "application/vnd.github.v3+json");

				var client = _clientFactory.CreateClient();
				client.DefaultRequestHeaders.Add("apikey", apiKey);

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

						if (count == 1)
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

		[HttpGet("GetExchnageRate")]
		public async Task<IActionResult> GetExchnageRate(string name)
		{
			var settings = await _context.Settings.ToListAsync();
			var baseCurrency = settings.Single(c => c.Key.Equals("Exchange_BaseCurrency")).Value;
			var apiKey = settings.Single(c => c.Key.Equals("Exchange_ApiLayerApiKey")).Value;
			var request = new HttpRequestMessage(HttpMethod.Get, string.Format("https://api.apilayer.com/exchangerates_data/latest?symbols={0}&base={1}", name, baseCurrency));
			request.Headers.Add("Accept", "application/vnd.github.v3+json");

			var client = _clientFactory.CreateClient();
			client.DefaultRequestHeaders.Add("apikey", apiKey);

			var response = await client.GetAsync(request.RequestUri);
			var contents = response.Content.ReadAsStringAsync().Result;
			dynamic stuff = JObject.Parse(contents);
			decimal rate = 0;
			foreach (var day in stuff.rates)
			{
				rate = day.First;
			}

			return Json(rate);
		}


		[HttpGet("GetRates")]
		public async Task<IActionResult> GetRates(int currency)
		{
			//var cu = await _context.Exchanges.FirstOrDefaultAsync(c => c.Id == currency);
			//var endDate = DateTime.Today.Year + "-" + DateTime.Today.Month.ToString("00") + "-" + DateTime.Today.Day.ToString("00");
			//var startDate = DateTime.Today.AddYears(-1).Year + "-" + DateTime.Today.AddYears(-1).Month.ToString("00") + "-" + DateTime.Today.AddYears(-1).Day.ToString("00");

			//var settings = await _context.Settings.ToListAsync();
			//var baseCurrency = settings.Single(c => c.Key.Equals("Exchange_BaseCurrency")).Value;
			//var apiKey = settings.Single(c => c.Key.Equals("Exchange_ApiLayerApiKey")).Value;

			//var request = new HttpRequestMessage(HttpMethod.Get, string.Format("https://api.apilayer.com/exchangerates_data/timeseries?start_date={0}&end_date={1}&symbols={2}&base={3}", startDate, endDate, cu.Symbol, baseCurrency));
			//request.Headers.Add("Accept", "application/vnd.github.v3+json");

			//var client = _clientFactory.CreateClient();
			//client.DefaultRequestHeaders.Add("apikey", apiKey);

			//var response = await client.GetAsync(request.RequestUri);
			//var contents = response.Content.ReadAsStringAsync().Result;
			//dynamic stuff = JObject.Parse(contents);

			var ch = await _context.ExchangeHistory.Where(c => c.ExchangeId == currency).ToListAsync();

			var exchangeRates = new List<Exchange>();
			var selectedRate = new Exchange();

			var ratesMaket = new Dictionary<DateTime, decimal>();
			var ratesSelling = new Dictionary<DateTime, decimal>();
			var ratesBuying = new Dictionary<DateTime, decimal>();

			//foreach (var day in stuff.rates)
			//{
			//	var date = DateTime.Parse(day.Name);
			//	foreach (var cur in day.First)
			//	{
			//		pastValues.Add(date, (decimal)cur.Value.Value);
			//	}
			//}
			foreach (var cur in ch)
			{
				ratesMaket.Add(cur.UpdatedDate, cur.MarketRate);
				ratesBuying.Add(cur.UpdatedDate, cur.BuyingRate);
				ratesSelling.Add(cur.UpdatedDate, cur.SellingRate);
			}

			var resultMarket = from t in ratesMaket
							   group t by new
							   {
								   t.Key.Year,
								   t.Key.Month
							   } into g
							   select new
							   {
								   Month = g.Key.Month.ToString("MMM"),
								   Avarage = Math.Round(g.Average(t => t.Value), 2)
							   };
			var resultSelling = from t in ratesSelling
								group t by new
								{
									t.Key.Year,
									t.Key.Month
								} into g
								select new
								{
									Month = g.Key.Month.ToString("MMM"),
									Avarage = Math.Round(g.Average(t => t.Value), 2)
								};
			var resultBuying = from t in ratesBuying
							   group t by new
							   {
								   t.Key.Year,
								   t.Key.Month
							   } into g
							   select new
							   {
								   Month = g.Key.Month.ToString("MMM"),
								   Avarage = Math.Round(g.Average(t => t.Value), 2)
							   };


			var months = Enumerable.Range(0, 12)
							  .Select(i => DateTime.Now.AddMonths(i - 11))
							  .Select(date => date.ToString("MMM")).ToArray();

			return Json(new ChartExchange()
			{
				RatesMarket = resultMarket.Select(d => d.Avarage).ToArray(),
				RatesBuying = resultBuying.Select(d => d.Avarage).ToArray(),
				RatesSelling = resultSelling.Select(d => d.Avarage).ToArray(),
				Months = months.ToArray()
			});
		}

	}
}
