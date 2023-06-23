using Microsoft.AspNetCore.Identity;

namespace AuthReadyAPI.DataLayer.Models
{
    public class APIUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
