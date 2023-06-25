using AuthReadyAPI.DataLayer.DTOs.APIUser;
using AuthReadyAPI.DataLayer.DTOs.Company;
using AuthReadyAPI.DataLayer.Models;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface ICompany : IGenericRepository<Company>
    {
        public Task<string> COMPANY__GIVE__ADMIN(Full__APIUser DTO);
    }
}
