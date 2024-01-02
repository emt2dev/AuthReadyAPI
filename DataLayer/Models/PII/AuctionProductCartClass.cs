using AuthReadyAPI.DataLayer.Models.ProductInfo;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.Models.PII
{
    public class AuctionProductCartClass
    {
        public int Id { get; set; }
        public AuctionProductClass Item { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public int CompanyId { get; set; }

        [ForeignKey(nameof(UserId))]
        public string UserId { get; set; }
        public bool Submitted { get; set; }
        public bool Abandoned { get; set; }
        public string Expiration { get; set; }
        public List<ProductUpsellItemClass> Upsells { get; set; }


        public AuctionProductCartClass()
        {
            
        }

        public bool HasExpired()
        {
            if (DateTime.Parse(Expiration) < DateTime.Now) return true;

            return false;
        }
    }
}
