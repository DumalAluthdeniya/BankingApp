using BankingApp.Models;

namespace BankingApp.ViewModels
{
    public class HomeViewModel
    {
        public List<Account>? Accounts { get; set; }
        public List<Loan>? Loans { get; set; }
        public ApplicationUser  User { get; set; }
        public decimal TotalBalance => Accounts.Sum(a => a.Balance);
    }
}
