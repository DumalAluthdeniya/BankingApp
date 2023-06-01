using System.ComponentModel.DataAnnotations;

namespace BankingApp.Models
{
    public enum LoanType
    {
        [Display(Name = "APR % Compound Monthly")]
		APRCM = 1,
		[Display(Name = "APR % Compound Yearly")]
		APRCY = 2,
    }
}
