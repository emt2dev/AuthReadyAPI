using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.Models.Companies
{
    public class PointOfContactClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Phone]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        // Fkey
        [ForeignKey(nameof(CompanyId))]
        public int CompanyId { get; set; }
    }
}
