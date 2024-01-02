using AuthReadyAPI.DataLayer.Models.ProductInfo;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.DTOs.Product
{
    public class AuctionProductDTO
    {
        public int Id { get; set; }

        public bool HasBeenPurchased { get; set; } // true = ordered
        public string Name { get; set; }
        public string Description { get; set; }
        public double Weight { get; set; }
        public double Area { get; set; }
        public List<string> Images { get; set; }


        [ForeignKey(nameof(CompanyId))]
        public int CompanyId { get; set; }

        public double ShippingCost { get; set; }
        public string Carrier { get; set; }
        public string Delivery { get; set; }
        public string TaxCode { get; set; }

        public double CurrentBidAmount { get; set; }
        public double StartingBidAmount { get; set; }
        public double AutoSellBidAmount { get; set; }
        public bool AcceptAutoSell { get; set; } // the company can auto accept a bid if bool and current bid matches auto sell
        public DateTime AuctionEnd { get; set; }
    }
}
