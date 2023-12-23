using AuthReadyAPI.DataLayer.DTOs.Company;
using AuthReadyAPI.DataLayer.DTOs.PII.APIUser;
using AuthReadyAPI.DataLayer.Models.Companies;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface ICompany
    {
        public Task<bool> NewCompany(NewCompanyDTO IncomingDTO);
        public Task<bool> NewContact(NewPointOfContactDTO IncomingDTO);
        public Task<List<CompanyDTO>> GetAPICompanyList();
    }
}
