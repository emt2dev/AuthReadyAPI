namespace AuthReadyAPI.DataLayer.DTOs.Product
{
    public class NewCouponDTO
    {
        public int ShippingId { get; set; }
        public int StyleId { get; set; }
        public string UserEmail { get; set; }


        public double Price { get; set; }
        public bool OverrideStylePrice { get; set; } // if true, override currentprice with price
    }
}
