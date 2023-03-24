using System.ComponentModel.DataAnnotations;

namespace BankingApp.Models
{
    public class SupportItem
    {
        [Key]
        public int Id { get; set; }
        public Support? Support { get; set; }
        public string? Message { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsFromSupport { get; set; }
        public ApplicationUser? Sender { get; set; }
    }
}
