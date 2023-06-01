using BankingApp.Models;

namespace BankingApp.ViewModels
{
    public class UserViewModel
    {
        public ApplicationUser? User { get; set; }
        public IFormFile? ProfileImage { get; set; }
    }
}
