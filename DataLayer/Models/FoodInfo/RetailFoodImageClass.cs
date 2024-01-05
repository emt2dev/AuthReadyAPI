using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.Models.FoodInfo
{
    public class RetailFoodImageClass
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }

        [ForeignKey(nameof(RetailFoodClassId))]
        public int RetailFoodClassId { get; set; }
    }
}
