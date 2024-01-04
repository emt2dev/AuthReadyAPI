namespace AuthReadyAPI.DataLayer.Models.PII
{
    public class BidClass
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int AuctionProductId { get; set; }
        public double Amount { get; set; }
        public DateTime Submitted { get; set; }
    }
}
