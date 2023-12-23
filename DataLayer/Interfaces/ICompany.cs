using AuthReadyAPI.DataLayer.DTOs.Company;
using AuthReadyAPI.DataLayer.DTOs.PII.APIUser;
using AuthReadyAPI.DataLayer.Models.Companies;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface ICompany
    {
        public Task<string> COMPANY__GIVE__ADMIN(APIUserDTO DTO);
    }
}
