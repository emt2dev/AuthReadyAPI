namespace AuthReadyAPI.DataLayer.Models
{
    public class ShippingOption
    {
        public int Id { get; set; }
        public string Name { get; set;}
        public string Description { get; set;}
        public double AreaInInches { get; set;}
        public double MaxWeightInPounds { get; set;}
        public double Cost {  get; set;}
    }
}
