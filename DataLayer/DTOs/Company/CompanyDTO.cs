using System.ComponentModel.DataAnnotations;

namespace AuthReadyAPI.DataLayer.DTOs.Company
{
    public class CompanyDTO
    {
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

        // For internal 
        public int PointOfContactId { get; set; }
    }
}
