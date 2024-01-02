namespace AuthReadyAPI.DataLayer.Models.PII
{
    public class BidClass
    {
        public string UserId { get; set; }
        public int AuctionProductId { get; set; }
        public double Amount { get; set; }
        public DateTime Submitted { get; set; }
    }
}
