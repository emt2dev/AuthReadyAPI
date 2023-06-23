using System.ComponentModel.DataAnnotations;

namespace AuthReadyAPI.DataLayer.DTOs.APIUser
{
    public class Full__APIUser : Base__APIUser
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
