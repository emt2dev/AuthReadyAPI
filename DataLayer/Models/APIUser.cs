using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.Models
{
    public class APIUser : IdentityUser
    {
        public string? Name { get; set; }
        public int? CompanyId { get; set; }
        public Boolean IsStaff { get; set; }
        public virtual IList<Cart>? Carts { get; set; }
        public virtual IList<Order>? Orders { get; set; }
    }
}
