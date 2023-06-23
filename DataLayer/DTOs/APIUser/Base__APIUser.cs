using System.ComponentModel.DataAnnotations;

namespace AuthReadyAPI.DataLayer.DTOs.APIUser
{
    public class Base__APIUser
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
