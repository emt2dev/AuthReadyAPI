using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Models.PII;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.Models.ProductInfo
{
    public class SingleProductClass
    {
        public int Id { get; set; }

        public bool HasBeenPurchased { get; set; } // true = ordered
        public string Name { get; set; }
        public string Description { get; set; }
        public double Weight { get; set; }
        public double Area { get; set; }
        public List<SingleProductImageClass> Images { get; set; }

        [ForeignKey(nameof(UserEmail))]
        public string UserEmail { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public int CompanyId { get; set; }

        public double TotalCost { get; set; } // Shipping + Style Cost
        public double ProductPrice { get; set; }
        public double ShippingCost { get; set; }
        public string Carrier { get; set; }
        public string Delivery { get; set; }
        public string TaxCode { get; set; }

        public SingleProductClass()
        {
            
        }

        public SingleProductClass(NewSingleProductDTO DTO)
        {
            Id = 0;
            HasBeenPurchased = false;
            Name = DTO.Name;
            Description = DTO.Description;
            Weight = DTO.Weight;
            Area = DTO.Area;
            Images = new List<SingleProductImageClass>();
            UserEmail = DTO.UserEmail;
            CompanyId = DTO.CompanyId;
            TotalCost = DTO.ProductPrice + DTO.ShippingCost;
            ProductPrice = DTO.ProductPrice;
            ShippingCost = DTO.ShippingCost;
            Carrier = DTO.Carrier;
            Delivery = DTO.Delivery;
        }

        public void AddImages(List<SingleProductImageClass> ImageList)
        {
            foreach (var item in ImageList)
            {
                Images.Add(item);
            }
        }
    }
}
