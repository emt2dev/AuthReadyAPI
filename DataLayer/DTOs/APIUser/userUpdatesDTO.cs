using System.ComponentModel.DataAnnotations;

namespace AuthReadyAPI.DataLayer.DTOs.APIUser
{
    public class userUpdatesDTO
    {
        public string email { get;set; }
        public int phoneNumber { get;set; }
        public string currentPassword { get;set; }
        public string newPassword { get;set; }
    }
}