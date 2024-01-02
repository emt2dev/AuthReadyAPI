using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Models.PII;
using AuthReadyAPI.DataLayer.Models.ProductInfo;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.DTOs.PII.Payments
{
    public class SingleProductCartDTO
    {
        public int Id { get; set; }
        public SingleProductDTO Item { get; set; }

        public int CompanyId { get; set; }
        public string UserId { get; set; }

        public bool Submitted { get; set; }
        public bool Abandoned { get; set; }
        public List<UpsellItemClass> Upsells { get; set; }
        public string UserEmail { get; set; }

        public SingleProductCartDTO()
        {
            
        }

        public SingleProductCartDTO(SingleProductCartClass Class)
        {
            Id = Class.Id;
            Item = null;
            CompanyId = Class.CompanyId;
            UserId = Class.UserId;
            Submitted = Class.Submitted;
            Abandoned = Class.Abandoned;
            Upsells = new List<UpsellItemClass>();
            UserEmail = "";
        }

        public void AddItemDTO(SingleProductDTO DTO)
        {
            UserEmail += DTO.UserEmail;
            Item = DTO;
        }
    }
}
