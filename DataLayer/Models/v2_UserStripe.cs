using Microsoft.AspNetCore.Identity;

namespace AuthReadyAPI.DataLayer.Models
{
    public class v2_UserStripe : IdentityUser
    {
        public string? name { get; set; }
        public string? stripeId { get; set; }
        // below are customer properties
        public string? addressStreet { get; set; }
        public string? addressSuite { get; set; }
        public string? addressCity { get; set; }
        public string? addressState { get; set; }
        public string? addressPostal_code { get; set; }
        public string? addressCountry = "US";
        public string currency = "usd";
        public Boolean livemode = false;
        // below are staff properties
        public int? companyId { get; set; }
        public string? position { get; set; }
        public Boolean giveAdminPrivledges { get; set; }
        public Boolean giveDeveloperPrivledges { get; set; }
        public string? longitude { get; set; }
        public string? latitude { get; set; }
        public string? coordinates { get; set; }
    }
}