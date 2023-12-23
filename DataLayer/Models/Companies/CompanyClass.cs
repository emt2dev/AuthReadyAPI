using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AuthReadyAPI.DataLayer.DTOs.Company;
using AuthReadyAPI.DataLayer.Models.PII;
using AuthReadyAPI.DataLayer.Models.ProductInfo;

namespace AuthReadyAPI.DataLayer.Models.Companies
{
    public class CompanyClass
    {
        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public string MailingAddress { get; set; }
        public string ShippingAddress { get; set; }

        // For Customers
        [EmailAddress]
        public string Email { get; set; }

        // For internal mailings
        [ForeignKey(nameof(PointOfContactId))]
        public int PointOfContactId { get; set; }
        public CompanyClass()
        {
            
        }
        public CompanyClass(NewCompanyDTO IncomingDTO)
        {
            Active = IncomingDTO.Active;
            Name = IncomingDTO.CompanyName;
            Description = IncomingDTO.Description;
            PhoneNumber = IncomingDTO.PhoneNumber;
            MailingAddress = IncomingDTO.MailingAddress;
            ShippingAddress = IncomingDTO.ShippingAddress;
            Email = IncomingDTO.ExternalEmail;
            PointOfContactId = 0;
        }
    }
}
