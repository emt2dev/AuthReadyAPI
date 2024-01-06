using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.Models.FoodInfo
{
    public class FoodImageClass
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }

        [ForeignKey(nameof(FoodProductClassId))]
        public int FoodProductClassId { get; set; }
    }
}
