using AuthReadyAPI.DataLayer.DTOs.APIUser;

namespace AuthReadyAPI.DataLayer.Models
{
    public class v2_OrderDTO
    {
        public int id { get; set; }
        public v2_ShoppingCartDTO cart { get; set; }
        public v2_StaffDTO? driver { get; set; }
        public Boolean delivery { get; set; }
        public DateTime? timeDelivered { get; set; }
        public Boolean pickedUpByCustomer { get; set; }
        public DateTime? timePickedUpByCustomer { get; set; }
        public Boolean orderCompleted { get; set; }
    }
}
