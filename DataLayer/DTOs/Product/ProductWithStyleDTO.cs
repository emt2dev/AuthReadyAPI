using AuthReadyAPI.DataLayer.Models;

namespace AuthReadyAPI.DataLayer.DTOs.Product
{
    public class ProductWithStyleDTO
    {
        ProductDTO Product { get; set; }
        List<StyleDTO> Styles { get; set; }

        public ProductWithStyleDTO(ProductDTO Product, List<StyleDTO> Styles)
        {
            this.Product = Product;
            this.Styles = Styles;
        }

        public ProductWithStyleDTO(ProductDTO Product, StyleDTO Style)
        {
            this.Product = Product;
            List<StyleDTO> AddedStyles = new List<StyleDTO> { Style };
            this.Styles = AddedStyles;
        }
    }
}
