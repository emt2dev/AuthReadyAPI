namespace AuthReadyAPI.DataLayer.DTOs.Order
{
    public class Full__Order : Base__Order
    {
        public Double Payment_Amount { get; set; }
        public DateTime Time__Submitted { get; set; }
    }
}
