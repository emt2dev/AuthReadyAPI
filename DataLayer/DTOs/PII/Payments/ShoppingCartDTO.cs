namespace AuthReadyAPI.DataLayer.DTOs.PII.Payments
{
    public class ShoppingCartDTO
    {
        public int Id { get; set; }
        public IList<CartItemDTO> Items { get; set; } = new List<CartItemDTO>();
        public bool Submitted { get; set; }
        public bool Abandoned { get; set; }
        public bool CouponApplied { get; set; }
        public double PriceBeforeCoupon { get; set; }
        public double PriceAfterCoupon { get; set; }

        // Fkeys
        public int CouponCodeId { get; set; }
        public string UserId { get; set; }
    }
}
