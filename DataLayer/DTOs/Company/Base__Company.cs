using System.ComponentModel.DataAnnotations;

namespace AuthReadyAPI.DataLayer.DTOs.Company
{
    public class Base__Company
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
