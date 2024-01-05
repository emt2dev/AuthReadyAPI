
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.DTOs.Product
{
    public class ProductWithStyleDTO
    {
        public int Id { get; set; }
        public ProductDTO Product { get; set; }
        public List<StyleDTO> Styles { get; set; }

    }
}
