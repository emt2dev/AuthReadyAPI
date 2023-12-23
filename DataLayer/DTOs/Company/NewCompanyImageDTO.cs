using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.DTOs.Company
{
    public class NewCompanyImageDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        // FKey
        [ForeignKey(nameof(CompanyId))]
        public int CompanyId { get; set; }
    }
}
