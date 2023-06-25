using AuthReadyAPI.DataLayer.DTOs.Cart;
using AuthReadyAPI.DataLayer.DTOs.Order;
using System.ComponentModel.DataAnnotations;

namespace AuthReadyAPI.DataLayer.DTOs.APIUser
{
    public class Full__APIUser : Base__APIUser
    {
        public string? Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string? CompanyId { get; set; }
        public Boolean IsStaff { get; set; }
        public virtual IList<Full__Cart>? Carts { get; set; }
        public virtual IList<Full__Order>? Orders { get; set; }
    }
}
