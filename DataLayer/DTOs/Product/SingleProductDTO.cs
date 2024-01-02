using AuthReadyAPI.DataLayer.Models.ProductInfo;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.DTOs.Product
{
    public class SingleProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Weight { get; set; }
        public double Area { get; set; }
        public List<string> Images { get; set; }

        public string UserEmail { get; set; }

        public int CompanyId { get; set; }
        public double TotalCost { get; set; }
        public double ProductPrice { get; set; }
        public double ShippingCost { get; set; }
        public string Carrier { get; set; }
        public string Delivery { get; set; }
        public string TaxCode { get; set; }

        public SingleProductDTO()
        {
            
        }

        public SingleProductDTO(SingleProductClass Class)
        {
            Id = Class.Id;
            Name = Class.Name;
            Description = Class.Description;
            Weight = Class.Weight;
            Area = Class.Area;
            Images = new List<string>();
            UserEmail = Class.UserEmail;
            CompanyId = Class.CompanyId;
            TotalCost = Class.TotalCost;
            ProductPrice = Class.ProductPrice;
            ShippingCost = Class.ShippingCost;
            Carrier = Class.Carrier;
            Delivery = Class.Delivery;
            TaxCode = Class.TaxCode;
        }

        public void AddImages(List<SingleProductImageClass> Images)
        {
            foreach (var item in Images)
            {
                this.Images.Add(item.ImageUrl);
            }
        }
    }
}
