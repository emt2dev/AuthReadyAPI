using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.Models.Companies
{
    public class CompanyImageClass
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        // FKey
        [ForeignKey(nameof(CompanyId))]
        public int CompanyId { get; set; }

        public CompanyImageClass()
        {
            
        }
    }
}
