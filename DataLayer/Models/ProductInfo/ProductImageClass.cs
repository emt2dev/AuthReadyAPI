using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.Models.ProductInfo
{
    public class ProductImageClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        // FKey
        [ForeignKey(nameof(StyleId))]
        public int StyleId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public int ProductId { get; set; }
    }
}
