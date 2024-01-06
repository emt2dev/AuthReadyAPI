using AuthReadyAPI.DataLayer.Models.FoodInfo;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.DTOs.Food
{
    public class FoodCartDTO
    {
        public int Id { get; set; }

        public bool Submitted { get; set; }
        public bool Abandoned { get; set; }
        public bool CouponApplied { get; set; }
        public double PriceBeforeCoupon { get; set; }
        public double PriceAfterCoupon { get; set; }
        public double Total { get; set; }

        public List<FoodProductClass> FoodProducts = new List<FoodProductClass>();

        public int CompanyId { get; set; }
        public int UserId { get; set; }
        public int CouponCodeId { get; set; }
    }
}
