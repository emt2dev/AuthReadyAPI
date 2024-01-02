using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Models.PII;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.Models.ProductInfo
{
    public class AuctionProductClass
    {
        public int Id { get; set; }

        public bool HasBeenPurchased { get; set; } // true = ordered
        public string Name { get; set; }
        public string Description { get; set; }
        public double Weight { get; set; }
        public double Area { get; set; }
        public List<AuctionProductImageClass> Images { get; set; }
        public List<BidClass> Bids { get; set; }


        [ForeignKey(nameof(CompanyId))]
        public int CompanyId { get; set; }

        public double ShippingCost { get; set; }
        public string Carrier { get; set; }
        public string Delivery { get; set; }
        public string TaxCode { get; set; }

        public double CurrentBidAmount { get; set; }

        [ForeignKey(nameof(CurrentBidderId))]
        public string CurrentBidderId { get; set; } // UserId
        public double StartingBidAmount { get; set; }
        public double AutoSellBidAmount { get; set; }
        public bool AcceptAutoSell { get; set; } // the company can auto accept a bid if bool and current bid matches auto sell

        public DateTime AuctionEnd { get; set; }

        public AuctionProductClass()
        {
            
        }

        public AuctionProductClass(NewAuctionProductDTO DTO)
        {
            Id = 0;
            HasBeenPurchased = false;
            Name = DTO.Name;
            Description = DTO.Description;
            Weight = DTO.Weight;
            Area = DTO.Area;
            Images = new List<AuctionProductImageClass>();
            CompanyId = DTO.CompanyId;
            ShippingCost = DTO.ShippingCost;
            Carrier = DTO.Carrier;
            Delivery = DTO.Delivery;
            TaxCode = DTO.TaxCode;
            CurrentBidAmount = 0.00;
            StartingBidAmount = DTO.StartingBidAmount;
            AutoSellBidAmount = DTO.AutoSellBidAmount;
            AcceptAutoSell = DTO.AcceptAutoSell;
            Bids = new List<BidClass>();
        }

        public void AddImages(List<AuctionProductImageClass> images)
        {
            Images.AddRange(images);
        }

        public List<BidClass> GetTopBids()
        {
            return Bids.OrderByDescending(x => x.Amount).Take(3).ToList();
        }
    }
}
