using System.ComponentModel.DataAnnotations;

namespace AuthReadyAPI.DataLayer.DTOs.APIUser
{
    public class v2_CustomerDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string? stripeId { get; set; }
        public string name { get; set; }
        public string description = "Created on SASNM";
        public string? addressStreet { get; set; }
        public string? addressCity { get; set; }
        public string? addressState { get; set; }
        public string? addressPostal_code { get; set; }
        public string currency = "usd";
        public Boolean livemode = false; // change to true for production
    }
}
