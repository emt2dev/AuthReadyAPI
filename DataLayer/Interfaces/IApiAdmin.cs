using AuthReadyAPI.DataLayer.DTOs.APIUser;
using AuthReadyAPI.DataLayer.DTOs.Company;
using AuthReadyAPI.DataLayer.Models;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface IApiAdmin : IGenericRepository<APIUser>
    {
        public Task<Base__Company> COMPANY__CREATE(Base__Company DTO);
    }
}
