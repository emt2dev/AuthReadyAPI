namespace AuthReadyAPI.DataLayer.DTOs.Order
{
    public class Full__Order : Base__Order
    {
        public string? Destination_latitude { get; set; }
        public string? Destination_longitude { get; set; }
        public Boolean Payment_Complete { get; set; }
        public Double Payment_Amount { get; set; }
        public DateTime Time__Submitted { get; set; }
    }
}
