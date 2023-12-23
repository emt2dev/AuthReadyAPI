using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.Models.ProductInfo
{
    public class DigitalOwnershipClass
    {
        public int Id { get; set; }

        // Fk
        [ForeignKey(nameof(DigitalOwnerUserId))]
        public string DigitalOwnerUserId { get; set; }
        public int DownloadCount { get; set; }
        public string ProductKey { get; set; } // Guid
        public bool Activated { get; set; }
    }
}
