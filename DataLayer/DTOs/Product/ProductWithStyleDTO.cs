
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.DTOs.Product
{
    public class ProductWithStyleDTO
    {
        
        public ProductDTO Product { get; set; }
        public List<StyleDTO> Styles { get; set; }

    }
}
