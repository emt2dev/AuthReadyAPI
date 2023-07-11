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
        public string name { get; set; }
        public string? position { get; set; }
        public Boolean giveAdminPrivledges { get; set; }
        public string? longitude { get; set; }
        public string? latitude { get; set; }
        public string? coordinates { get; set; }
    }
}
