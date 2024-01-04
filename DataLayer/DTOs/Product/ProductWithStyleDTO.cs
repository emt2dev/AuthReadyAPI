
namespace AuthReadyAPI.DataLayer.DTOs.Product
{
    public class ProductWithStyleDTO
    {
        public int Id { get; set; }
        ProductDTO Product { get; set; }
        List<StyleDTO> Styles { get; set; }

        public ProductWithStyleDTO()
        {
            
        }
        public ProductWithStyleDTO(ProductDTO Product, List<StyleDTO> Styles)
        {
            Id = 0;
            this.Product = Product;
            this.Styles = Styles;
        }

        public ProductWithStyleDTO(ProductDTO Product, StyleDTO Style)
        {
            Id = 0;
            this.Product = Product;
            List<StyleDTO> AddedStyles = new List<StyleDTO> { Style };
            this.Styles = AddedStyles;
        }
    }
}
