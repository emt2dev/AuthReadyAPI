using AuthReadyAPI.DataLayer.DTOs.Product;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.DTOs.Order
{
    public class Base__Order
    {
        public string? Id { get; set; }
        public string? deliveryDriver_Id { get; set; }
        public string? Customer_Id { get; set; }
        public string? CompanyId { get; set; }
        public string? CartId { get; set; }
        public virtual IList<Full__Product> Products { get; set; }
        public string DestinationAddress { get; set; }
        public string CurrentStatus { get; set; }
        public DateTime? Time__Touched { get; set; }
        public DateTime? Time__Delivered { get; set; }
        public Boolean Payment_Complete { get; set; }
    }
}
