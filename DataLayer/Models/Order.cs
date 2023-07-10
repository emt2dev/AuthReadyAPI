using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string? delivery_driver_name { get; set; }
        public string? delivery_driver_longitdte { get; set; }
        public string? delivery_driver_latitude { get; set; }
        public string shoppingCartId { get; set; }
        public string? DestinationAddress { get; set; }
        public string? Destination_latitude { get; set; }
        public string? Destination_longitude { get; set; }
        public string CurrentStatus { get; set; }
        public DateTime? Time__Touched { get; set; }
        public DateTime? Time__Delivered { get; set; }
        public Boolean Payment_Complete { get; set; }
        public Boolean delivery { get; set; }
        public Double Payment_Amount { get; set; }
        public DateTime Time__Submitted { get; set; }
    }
}
