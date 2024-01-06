using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.Models.FoodInfo
{
    public class FoodProductClass
    {
        public int Id { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string TaxCode { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
    }
}
