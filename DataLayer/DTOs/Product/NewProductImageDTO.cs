using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.DTOs.Product
{
    public class NewProductImageDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        // FKey
        public int StyleId { get; set; }
        public int ProductId { get; set; }
    }
}
