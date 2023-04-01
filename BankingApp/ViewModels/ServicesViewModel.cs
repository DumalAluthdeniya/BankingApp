using BankingApp.Models;

namespace BankingApp.ViewModels
{
    public class ServicesViewModel
    {
        public Loan? Loan { get; set; }
        public List<Loan>? LoanRequests { get; set; }
        public int ActiveTab { get; set; }
    }
}
