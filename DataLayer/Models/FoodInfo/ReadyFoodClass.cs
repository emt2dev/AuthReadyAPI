using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.Models.FoodInfo
{
    public class ReadyFoodClass
    {
        [Key]
        public int Id { get; set; } // Primary Key
        // Information
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ReadyFoodImageClass> Images { get; set; }


        [ForeignKey(nameof(CompanyId))]
        public int CompanyId { get; set; }
        // Used for Cart
        public int Quantity = 1;
        public double Price { get; set; }
    }
}
