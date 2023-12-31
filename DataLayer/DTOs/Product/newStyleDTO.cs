namespace AuthReadyAPI.DataLayer.DTOs.Product
{
    public class NewStyleDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double CurrentPrice { get; set; }

        // Urgency
        public double DiscountedPrice { get; set; }
        public bool UseDiscountPrice { get; set; }
        public bool DigitalOnly { get; set; }

        // Shipping Details
        public double PackagedDimensions { get; set; } // Length * Width * Height
        public double PackagedWeight { get; set; }

        // FKeys
        public int ProductId { get; set; }
    }
}
