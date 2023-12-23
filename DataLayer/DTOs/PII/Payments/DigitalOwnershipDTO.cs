using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.DTOs.PII.Payments
{
    public class DigitalOwnershipDTO
    {
        public int Id { get; set; }

        // Fk
        public string DigitalOwnerUserId { get; set; }
        public int DownloadCount { get; set; }
        public string ProductKey { get; set; } // Guid
        public bool Activated { get; set; }
    }
}
