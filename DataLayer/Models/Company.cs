using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.Models
{
    public class Company
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Id_admin_one { get; set; }
        public string? Id_admin_two { get; set; }
        public virtual IList<APIUser>? APIUsers { get; set; }
        public virtual IList<ProductClass>? Products { get; set; }
        public virtual IList<Order>? Orders { get; set; }
        public virtual IList<Cart>? Carts { get; set; }
    }
}
