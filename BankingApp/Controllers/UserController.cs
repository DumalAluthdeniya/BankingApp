using BankingApp.Data;
using BankingApp.Models;
using BankingApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{

    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public UserController(ILogger<UserController> logger, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                return View(new UserViewModel()
                {
                    User = currentUser
                });

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
