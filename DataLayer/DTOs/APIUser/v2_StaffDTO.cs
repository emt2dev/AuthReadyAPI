using System.ComponentModel.DataAnnotations;

namespace AuthReadyAPI.DataLayer.DTOs.APIUser
{
    public class v2_StaffDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string id { get;set; }
        public string name { get; set; }
        public string? position { get; set; }
        public string? addressStreet { get; set; }
        public string? addressSuite { get; set; }
        public string? addressCity { get; set; }
        public string? addressState { get; set; }
        public string? addressPostal_code { get; set; }
        public string? addressCountry { get; set; }
        public Boolean giveAdminPrivledges { get; set; }
        public string? longitude { get; set; }
        public string? latitude { get; set; }
        public string? coordinates { get; set; }
    }
}
