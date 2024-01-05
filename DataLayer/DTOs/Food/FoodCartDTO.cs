using AuthReadyAPI.DataLayer.Models.FoodInfo;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.DTOs.Food
{
    public class FoodCartDTO
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string UserId { get; set; }

        public List<RetailFoodDTO> RetailFood = new List<RetailFoodDTO>();
        public List<ReadyFoodDTO> ReadyFood = new List<ReadyFoodDTO>();
        public double Total { get; set; }
    }
}
