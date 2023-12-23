using AuthReadyAPI.DataLayer.DTOs.PII.APIUser;
using AuthReadyAPI.DataLayer.Models.PII;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface IUser
    {
        public Task<string> USER__CREATE(APIUserDTO user);
        
        public Task<APIUserClass> USER__FIND__BY__EMAIL__ASYNC(string? email);
    }
}
