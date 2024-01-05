using AuthReadyAPI.DataLayer.Models.FoodInfo;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.DTOs.Food
{
    public class ReadyFoodDTO
    {
        [Key]
        public int Id { get; set; } // Primary Key
        // Information
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Images { get; set; }


        [ForeignKey(nameof(CompanyId))]
        public int CompanyId { get; set; }
        // Used for Cart
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
