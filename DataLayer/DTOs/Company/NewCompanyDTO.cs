using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.DTOs.Company
{
    public class NewCompanyDTO
    {
        public bool Active { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public string MailingAddress { get; set; }
        public string ShippingAddress { get; set; }

        // For Customers
        [EmailAddress]
        public string ExternalEmail { get; set; }
    }
}
