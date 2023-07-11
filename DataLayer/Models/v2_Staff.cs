using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.Models
{
    public class v2_Staff : IdentityUser
    {
        public string name { get; set; }
        public string? position { get; set; }
        public Boolean giveAdminPrivledges { get; set; }
        public Boolean giveDeveloperPrivledges { get; set; }
        public string? longitude { get; set; }
        public string? latitude { get; set; }
        public string? coordinates { get; set; }
    }
}