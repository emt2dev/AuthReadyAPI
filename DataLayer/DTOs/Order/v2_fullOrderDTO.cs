using AuthReadyAPI.DataLayer.DTOs.APIUser;
using AuthReadyAPI.DataLayer.DTOs.Product;

namespace AuthReadyAPI.DataLayer.Models
{
    public class v2_fullOrderDTO
    {
        public int id { get; set; }
        public IList<v2_ProductDTO> items { get; set; }
        public Boolean delivery { get; set; }
        public string? deliveryAddress { get;set; }
        public DateTime? timeDelivered { get; set; }
        public Boolean pickedUpByCustomer { get; set; }
        public DateTime? timePickedUpByCustomer { get; set; }
        public Boolean orderCompleted { get; set; }
        public string status { get; set; }
        public string? eta { get; set; }
        public string method { get; set; }
    }
}
