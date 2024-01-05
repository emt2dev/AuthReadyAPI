using Stripe.Checkout;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.Models.PII
{
    public class PreparedCartClass
    {
        public int Id { get; set; }
        public string CartType { get; set; }
        public int CartId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public int CompanyId { get; set; }
    }
}
