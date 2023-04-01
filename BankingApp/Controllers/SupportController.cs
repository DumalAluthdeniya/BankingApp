using BankingApp.Data;
using BankingApp.Models;
using BankingApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Controllers
{
    public class SupportController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SupportController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(new SupportViewModel()
            {
                Support = new Support()
                {
                    Customer = await _userManager.GetUserAsync(HttpContext.User)
                }
            });
        }

        public async Task<IActionResult> AddSupportTicketAsync(SupportViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var lastTicket = await _context.Supports.OrderByDescending(d => d.Id).FirstAsync();

            model.Support.Ticket = "SIF" + (lastTicket.Id + 1).ToString().PadLeft(4, '0');
            model.Support.Customer = currentUser;

            var sItem = new SupportItem()
            {
                Support = model.Support,
                IsFromSupport = false,
                Message = model.Message,
                SendDate = DateTime.Now,
                Sender = currentUser
            };

            await _context.AddAsync(sItem);
            await _context.SaveChangesAsync();


            return RedirectToAction("Index");
        }
    }
}
