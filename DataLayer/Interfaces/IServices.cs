using AuthReadyAPI.DataLayer.DTOs.Services;
using AuthReadyAPI.DataLayer.Models.ServicesInfo;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface IServices
    {
        public Task<List<ServicesDTO>> GetCompanyServicesOffered(int CompanyId);
        public Task<List<ServicesDTO>> FindCompanyServicesOffered(int CompanyId, string Description);
        public Task<bool> AddService(NewServicesDTO DTO);
        public Task<bool> UpdateService(ServicesDTO DTO);
        public Task<bool> DeleteService(int ServiceId);
        public Task<bool> ScheduleAppointment(NewAppointmentDTO DTO); // this should be called after submitting payment
        public Task<bool> CustomerArrived(AppointmentShowDTO DTO);
        public Task<string> SubmitServicesOrder(AppointmentClass Obj);
    }
}
