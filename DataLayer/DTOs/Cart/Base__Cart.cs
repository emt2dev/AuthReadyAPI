using AuthReadyAPI.DataLayer.DTOs.Product;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.DTOs.Cart
{
    public class Base__Cart
    {
        public string Id { get; set; }
        public string Customer_Id { get; set; }

        public string CompanyId { get; set; }
        public virtual IList<Full__Product>? Products { get; set; }
        public double Total_Amount { get; set; }
        public Boolean Abandoned { get; set; }
        public Boolean Submitted { get; set; }
    }
}
