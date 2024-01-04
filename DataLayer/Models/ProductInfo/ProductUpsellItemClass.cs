using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.Models.ProductInfo
{
    public class ProductUpsellItemClass
    {
        public int Id { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public int CompanyId { get; set; }

        public int Quantity { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double CostWithShipping { get; set; }
        public bool DigitalOnly { get; set; }
        public string ImageUrl { get; set; }
        public string TaxCode { get; set; }
    }
}
