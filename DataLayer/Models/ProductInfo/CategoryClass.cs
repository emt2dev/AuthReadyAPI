using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.Models.ProductInfo
{
    public class CategoryClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey(nameof(CompanyId))]
        public int CompanyId { get; set; }
    }
}
