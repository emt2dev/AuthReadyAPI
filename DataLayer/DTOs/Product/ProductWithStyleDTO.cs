using AuthReadyAPI.DataLayer.Models;

namespace AuthReadyAPI.DataLayer.DTOs.Product
{
    public class ProductWithStyleDTO
    {
        ProductDTO Product { get; set; }
        StyleDTO Style { get; set; }

        public ProductWithStyleDTO(ProductDTO Product, StyleDTO Style)
        {
            this.Product = Product;
            this.Style = Style;
        }
    }
}
