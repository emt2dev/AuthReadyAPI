using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.DTOs.PII.APIUser
{
    public class APIUserDTO : IdentityUser
    {
        // Info
        [Required]
        public string Name { get; set; }
        public string BillingAddress { get; set; }
        public string MailingAddress { get; set; }

        // Metrics
        public double LifetimeSpent { get; set; } // how much they've purchased throughout their account history
        public int OrderCount { get; set; }

        // Fkeys
        [ForeignKey(nameof(CompanyId))]
        public int CompanyId { get; set; }
    }
}
