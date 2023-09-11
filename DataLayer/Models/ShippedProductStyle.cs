using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.Models
{
    public class ShippedProductStyle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInInches { get; set; }
        public double WidthInInches { get; set; }
        public double HeightInInches { get; set; }
        public double AreaInInches { get; set; }
        public double WeightInPounds { get; set; }
        [NotMapped]
        public IList<string> ImageUrls { get; set; }
    }
}
