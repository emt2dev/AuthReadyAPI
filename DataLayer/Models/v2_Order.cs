using System.ComponentModel.DataAnnotations;

namespace AuthReadyAPI.DataLayer.Models
{
    public class v2_Order
    {
        [Key]
        public int Id { get; set; }
        public v2_ShoppingCart cart { get; set; }
        public v2_Staff? Driver { get; set; }
        public Boolean delivery { get; set; }
        public DateTime? timeDelivered { get; set; }
        public Boolean pickedUpByCustomer { get; set; }
        public DateTime? timePickedUpByCustomer { get; set; }
        public Boolean orderCompleted { get; set; }
    }
}
