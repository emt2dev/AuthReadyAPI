using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.Models
{
    public class v2_CustomerStripe : IdentityUser
    {
        public string? stripeId { get; set; }
        public string name { get; set; }
        public string? addressStreet { get; set; }
        public string? addressCity { get; set; }
        public string? addressState { get; set; }
        public string? addressPostal_code { get; set; }
        public string? addressCountry = "US";
        public string currency = "usd";
        public Boolean livemode = false;
        public string? here;
    }
}