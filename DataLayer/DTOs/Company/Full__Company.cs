using AuthReadyAPI.DataLayer.DTOs.APIUser;
using AuthReadyAPI.DataLayer.DTOs.Cart;
using AuthReadyAPI.DataLayer.DTOs.Order;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.DTOs.Company
{
    public class Full__Company : Base__Company
    {
        public string? Id_admin_one { get; set; }
        public string? Id_admin_two { get; set; }
        public virtual IList<Full__APIUser>? StaffList { get; set; }
        public virtual IList<Full__Product>? Products { get; set; }
        public virtual IList<Full__Order>? Orders { get; set; }
        public virtual IList<Full__Cart>? Carts { get; set; }
    }
}
