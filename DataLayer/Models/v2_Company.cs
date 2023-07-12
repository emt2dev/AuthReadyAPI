using System.ComponentModel.DataAnnotations;

namespace AuthReadyAPI.DataLayer.Models
{
    public class v2_Company
    {
        [Key]
        public int id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public string? phoneNumber { get; set; }
        public string? addressStreet { get; set; }
        public string? addressSuite { get; set; }
        public string? addressCity { get; set; }
        public string? addressState { get; set; }
        public string? addressPostal_code { get; set; }
        public string? addressCountry { get; set; }
        public string? smallTagline { get; set; }
        public string? menuDescription { get; set; }
        public string? headerImage { get; set; }
        public string? aboutUsImageUrl { get; set; }
        public string? locationImageUrl { get; set; }
        public string? logoImageUrl { get; set; }
        public string? miscImageUrl { get; set; }
        public IList<v2_ProductStripe>? listOfAllProducts { get; set; }
        public v2_Staff? owner { get; set; }
        public v2_Staff? administratorOne { get; set; }
        public v2_Staff? administratorTwo { get; set; }
    }
}
