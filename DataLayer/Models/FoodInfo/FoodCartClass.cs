using AuthReadyAPI.DataLayer.DTOs.Food;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.Models.FoodInfo
{
    public class FoodCartClass
    {
        public int Id { get; set; }

        public bool Submitted { get; set; }
        public bool Abandoned { get; set; }
        public bool CouponApplied { get; set; }
        public double PriceBeforeCoupon { get; set; }
        public double PriceAfterCoupon { get; set; }
        public double Total { get; set; }

        public List<FoodProductClass> FoodProducts = new List<FoodProductClass>();

        [ForeignKey(nameof(CompanyId))]
        public int CompanyId { get; set; }

        [ForeignKey(nameof(UserId))]
        public int UserId { get; set; }
        [ForeignKey(nameof(CouponCodeId))]
        public int CouponCodeId { get; set; }
    }
}
