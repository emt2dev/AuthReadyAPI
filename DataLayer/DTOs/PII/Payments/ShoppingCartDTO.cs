using AuthReadyAPI.DataLayer.Models.PII;
using AuthReadyAPI.DataLayer.Models.ProductInfo;

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
        public double ShippingCost { get; set; }
        public List<UpsellItemClass> Upsells { get; set; }

        // Fkeys
        public int CouponCodeId { get; set; }
        public string UserId { get; set; }

        // Shipping Options
        public int PackageCount { get; set; }
        public List<ShippingInfoDTO> GeneratedShippingInfo { get; set; }
        public List<ShippingInfoDTO> AvailableShippingOptions { get; set; }

        public ShoppingCartDTO()
        {
            
        }

        public ShoppingCartDTO(ShoppingCartClass Obj, List<CartItemDTO> Items, List<ShippingInfoDTO> Generated, int PackageCount, double ShippingCost)
        {
            Id = Obj.Id;
            this.Items = Items;
            Submitted = Obj.Submitted;
            Abandoned = Obj.Abandoned;
            CouponApplied = Obj.CouponApplied;
            PriceAfterCoupon = Obj.PriceAfterCoupon;
            CouponCodeId = Obj.CouponCodeId;
            UserId = Obj.UserId;
            this.PackageCount = PackageCount;
            Upsells = new List<UpsellItemClass>();

            GeneratedShippingInfo = Generated;

            this.ShippingCost = ShippingCost;
        }
    }
}
