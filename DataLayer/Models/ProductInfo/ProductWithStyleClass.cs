using AuthReadyAPI.DataLayer.DTOs.Product;

namespace AuthReadyAPI.DataLayer.Models.ProductInfo
{
    public class ProductWithStyleClass
    {
        public ProductClass Product { get; set; }
        public List<StyleClass> Styles { get; set; }
    }
}
