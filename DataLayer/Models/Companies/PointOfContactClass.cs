using AuthReadyAPI.DataLayer.DTOs.Company;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.Models.Companies
{
    public class PointOfContactClass
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [Phone]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        // Fkey
        [ForeignKey(nameof(CompanyId))]
        public int CompanyId { get; set; }
        public PointOfContactClass()
        {

        }
        public PointOfContactClass(int CompanyId, PointOfContactDTO IncomingDTO)
        {
            this.CompanyId = CompanyId;
            Name = IncomingDTO.Name;
            Phone = IncomingDTO.Phone;
            Email = IncomingDTO.Email;
        }


    }
}
