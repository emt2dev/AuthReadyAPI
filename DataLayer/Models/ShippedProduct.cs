namespace AuthReadyAPI.DataLayer.Models
{
    public class ShippedProduct : Product
    {
        public bool IsAvailable { get; set; }
        public double LengthInInches { get; set; }
        public double WidthInInches { get; set; }
        public double HeightInInches { get; set; }
        public double AreaInInches { get; set; }
        public double WeightInPounds { get; set; }
        public IList<ShippedProductStyle> Styles { get; set; }
    }
}
