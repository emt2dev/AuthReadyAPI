using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.Models.ProductInfo
{
    public class SingleProductImageClass
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        [ForeignKey(nameof(SingleProductId))]
        public int SingleProductId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public int CompanyId { get; set; }
    }
}
