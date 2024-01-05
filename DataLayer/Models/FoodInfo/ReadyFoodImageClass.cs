using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.Models.FoodInfo
{
    public class ReadyFoodImageClass
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }

        [ForeignKey(nameof(ReadyFoodClassId))]
        public int ReadyFoodClassId { get; set; }
    }
}
