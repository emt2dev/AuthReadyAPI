using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.Models.PII
{
    public class APIUserClass : IdentityUser
    {
        // Customer Info
        [Required]
        public string Name { get; set; }
        public string ShippingAddress { get; set; }
        public string MailingAddress { get; set; }

        // Metrics
        public double LifetimeSpent { get; set; } // how much they've purchased throughout their account history
        public int OrderCount { get; set; }

        // Fkeys
        [ForeignKey(nameof(CompanyId))]
        public int CompanyId { get; set; }
    }
}
