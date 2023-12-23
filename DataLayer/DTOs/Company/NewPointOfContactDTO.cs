using System.ComponentModel.DataAnnotations;

namespace AuthReadyAPI.DataLayer.DTOs.Company
{
    public class NewPointOfContactDTO
    {
        public string Name { get; set; }
        [Phone]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        // Fkey
        public int CompanyId { get; set; }
    }
}
