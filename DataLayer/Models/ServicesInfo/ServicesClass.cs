using AuthReadyAPI.DataLayer.DTOs.Services;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.Models.ServicesInfo
{
    public class ServicesClass
    {
        public int Id { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TaxCode { get; set; }
        public double CurrentPrice { get; set; }
        public int Quantity { get; set; }
        public double DiscountedPrice { get; set; }
        public bool UseDiscountedPrice { get; set; }

        public ServicesClass()
        {
            
        }

        public ServicesClass(NewServicesDTO DTO)
        {
            Id = 0;
            Name = DTO.Name;
            Description = DTO.Description;
            TaxCode = DTO.TaxCode;
            CurrentPrice = DTO.CurrentPrice;
            DiscountedPrice = DTO.DiscountedPrice;
            UseDiscountedPrice = DTO.UseDiscountedPrice;
            Quantity = 1;
            CompanyId = DTO.CompanyId;
        }

        public void ApplyDiscount(bool decision)
        {
            UseDiscountedPrice = decision;
        }

        public void SetDiscountedPrice(double discountedPrice)
        {
            DiscountedPrice = discountedPrice;
        }
    }
}
