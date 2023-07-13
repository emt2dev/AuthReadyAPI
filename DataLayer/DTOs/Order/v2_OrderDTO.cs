using AuthReadyAPI.DataLayer.DTOs.APIUser;

namespace AuthReadyAPI.DataLayer.Models
{
    public class v2_OrderDTO
    {
        public int id { get; set; }
        public v2_ShoppingCart cart { get; set; }
        public v2_Staff? Driver { get; set; }
        public Boolean delivery { get; set; }
        public string? deliveryAddress { get;set; }
        public DateTime? timeDelivered { get; set; }
        public Boolean pickedUpByCustomer { get; set; }
        public DateTime? timePickedUpByCustomer { get; set; }
        public Boolean orderCompleted { get; set; }
        public string status { get; set; }
        public string eta { get; set; }
        public string method { get; set; }
    }
}
