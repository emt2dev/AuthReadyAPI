using System.ComponentModel.DataAnnotations.Schema;
using AuthReadyAPI.DataLayer.DTOs.Product;

namespace AuthReadyAPI.DataLayer.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public string Company { get; set; }
        public IList<Full__Product> Products { get; set; } = new List<Full__Product>();
        public double Total_Amount { get; set; }
        public double Total_Discounted { get; set; }
        public int Discount_Rate { get; set; }
        public Boolean Abandoned { get; set; }
        public Boolean Submitted { get; set; }
    }
}
