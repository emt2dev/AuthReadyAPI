using AuthReadyAPI.DataLayer.Models.ProductInfo;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.Models.PII
{
    public class SingleProductCartClass
    {
        public int Id { get; set; }
        public SingleProductClass Item { get; set; }

        [ForeignKey(nameof(UserId))]
        public string UserId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public int CompanyId { get; set; }
        public bool Submitted { get; set; }
        public bool Abandoned { get; set; }
        public List<ProductUpsellItemClass> Upsells { get; set; }
    }
}
