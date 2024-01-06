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

        public List<RetailFoodDTO> RetailFood = new List<RetailFoodDTO>();
        public List<ReadyFoodDTO> ReadyFood = new List<ReadyFoodDTO>();

        public int CompanyId { get; set; }
        public string UserId { get; set; }
        public int CouponCodeId { get; set; }
    }
}
