using AuthReadyAPI.DataLayer.DTOs.Services;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface IServices
    {
        public Task<List<ServicesDTO>> GetCompanyServicesOffered(int CompanyId);
        public Task<List<ServicesDTO>> FindCompanyServicesOffered(int CompanyId, string Description);
        public Task<bool> AddService(NewServicesDTO DTO);
        public Task<bool> UpdateService(ServicesDTO DTO);
        public Task<bool> DeleteService(int ServiceId);
    }
}
