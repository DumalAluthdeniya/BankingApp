using System.ComponentModel.DataAnnotations;

namespace BankingApp.Models
{
    public enum PermissionType
    {
        View = 0,
        [Display(Name ="View & Update")]
        Update = 1,
        Restricted = 2,
    }
}
