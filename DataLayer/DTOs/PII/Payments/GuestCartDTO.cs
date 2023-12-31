using AuthReadyAPI.DataLayer.DTOs.Product;

namespace AuthReadyAPI.DataLayer.DTOs.PII.Payments
{
    public class GuestCartDTO
    {
        public bool CouponApplied { get; set; }
        public double PriceBeforeCoupon { get; set; }
        public double PriceAfterCoupon { get; set; }
        public double ShippingCost { get; set; }
        List<AddProductToCartDTO> Items { get; set; }
    }
}
