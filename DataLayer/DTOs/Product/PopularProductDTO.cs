using AuthReadyAPI.DataLayer.Models;

namespace AuthReadyAPI.DataLayer.DTOs.Product
{
    public class PopularProductDTO
    {
        ProductDTO Product { get; set; }
        StyleDTO Style { get; set; }

        public PopularProductDTO(ProductDTO Product, StyleDTO Style)
        {
            this.Product = Product;
            this.Style = Style;
        }
    }
}
