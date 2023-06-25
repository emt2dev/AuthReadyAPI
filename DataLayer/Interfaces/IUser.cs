using AuthReadyAPI.DataLayer.DTOs.APIUser;
using AuthReadyAPI.DataLayer.Models;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface IUser : IGenericRepository<APIUser>
    {
        public Task<string> USER__CREATE(Full__APIUser user);
        
        public Task<APIUser> USER__FIND__BY__EMAIL__ASYNC(string? email);
    }
}
