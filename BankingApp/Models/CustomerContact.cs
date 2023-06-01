
using System.ComponentModel.DataAnnotations;

namespace BankingApp.Models
{
    public class CustomerContact
    {
        [Key]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public ApplicationUser? Customer { get; set; }
        public ApplicationUser? ContactUser { get; set; }
    }
}
