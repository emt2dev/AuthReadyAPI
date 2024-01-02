using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AuthReadyAPI.DataLayer.DTOs.PII;
using AuthReadyAPI.DataLayer.Models.ProductInfo;

namespace AuthReadyAPI.DataLayer.Models.PII
{
    public class ShoppingCartClass
    {
        [Key]
        public int Id { get; set; }
        public IList<CartItemClass> Items { get; set; } = new List<CartItemClass>();
        public List<UpsellItemClass> Upsells { get; set; }
        public bool Submitted { get; set; }
        public bool Abandoned { get; set; }
        public bool CouponApplied { get; set; }
        public double PriceBeforeCoupon { get; set; }
        public double PriceAfterCoupon { get; set; }

        // Fkeys
        [ForeignKey(nameof(CouponCodeId))]
        public int CouponCodeId { get; set; }

        [ForeignKey(nameof(UserId))]
        public string UserId { get; set; }

        public ShoppingCartClass()
        {
            
        }
    }
}