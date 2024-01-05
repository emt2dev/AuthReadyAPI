using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.Models.FoodInfo
{
    public class FoodCartClass
    {
        public int Id { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public int CompanyId { get; set; }

        [ForeignKey(nameof(UserId))]
        public string UserId { get; set; }

        public List<RetailFoodClass> RetailFood = new List<RetailFoodClass>();
        public List<ReadyFoodClass> ReadyFood = new List<ReadyFoodClass>();
        public double Total { get; set; }
    }
}
