using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Models.ProductInfo;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.DTOs.PII.Payments
{
    public class AuctionProductCartDTO
    {
        public int Id { get; set; }
        public AuctionProductDTO Item { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public int CompanyId { get; set; }

        [ForeignKey(nameof(UserId))]
        public string UserId { get; set; }
        public bool Submitted { get; set; }
        public bool Abandoned { get; set; }
        public string Expiration { get; set; }
        public List<UpsellItemClass> Upsells { get; set; }
        public string UserEmail { get; set; }
    }
}
