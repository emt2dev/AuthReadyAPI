using AuthReadyAPI.DataLayer.DTOs.APIUser;
using AuthReadyAPI.DataLayer.DTOs.Company;
using AuthReadyAPI.DataLayer.Models;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface IApiAdmin : IGenericRepository<APIUser>
    {
        public Task<string> API__ADMIN__CREATE(Full__APIUser DTO);
        public Task<Full__Company> COMPANY__CREATE(Full__Company DTO);
    }
}
