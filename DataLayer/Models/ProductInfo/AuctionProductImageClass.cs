using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.Models.ProductInfo
{
    public class AuctionProductImageClass
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        [ForeignKey(nameof(AuctionProductId))]
        public int AuctionProductId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public int CompanyId { get; set; }
    }
}
