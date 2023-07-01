using System.ComponentModel.DataAnnotations;

namespace AuthReadyAPI.DataLayer.DTOs.AuthResponse
{
    public class overrideDTO
    {
        public string? userEmail { get; set; }
        public int companyId { get; set; }
        public int replaceAdminOneOrTwo { get; set; }
    }
}