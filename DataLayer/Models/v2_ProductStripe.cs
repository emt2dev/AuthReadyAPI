using System.ComponentModel.DataAnnotations;

namespace AuthReadyAPI.DataLayer.Models
{
    public class v2_ProductStripe
    {
        [Key]
        public int id { get; set; }
        public string? stripeId { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public long? default_price { get; set; }
        public Boolean livemode = false;
        public string? package_dimensions { get; set; }
        public string? statement_descriptor { get; set; } // displayed on invoice
        public string? unit_label { get; set; }
        public Boolean shippable = false;
        public IList<string>? images { get; set; } // array of imageUrls
        public string? url { get; set; }
        public string? priceInString { get; set; }
        public IList<string>? seo { get; set; }
        public string? keyword { get; set; }
    }
}
