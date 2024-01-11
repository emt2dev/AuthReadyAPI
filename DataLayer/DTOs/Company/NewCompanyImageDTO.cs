using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.DTOs.Company
{
    public class NewCompanyImageDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<IFormFile> Images { get; set; }

        // FKey
        [ForeignKey(nameof(CompanyId))]
        public int CompanyId { get; set; }
    }
}
