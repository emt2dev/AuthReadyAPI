using AuthReadyAPI.DataLayer.DTOs.PII.Payments;
using AuthReadyAPI.DataLayer.DTOs.Services;
using AuthReadyAPI.DataLayer.Models.ServicesInfo;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface IServicesRepository
    {
        public Task<List<ServicesDTO>> GetCompanyServicesOffered(int CompanyId);
        public Task<List<ServicesDTO>> FindCompanyServicesOffered(int CompanyId, string Description);
        public Task<bool> AddService(NewServicesDTO DTO);
        public Task<bool> UpdateService(ServicesDTO DTO);
        public Task<bool> DeleteService(int ServiceId);
        public Task<List<AppointmentClass>> ScheduleAppointment(NewAppointmentDTO DTO); // this should be called after submitting payment
        public Task<bool> CustomerArrived(AppointmentShowDTO DTO);
        public string SubmitServicesOrder(AppointmentClass Obj);
        public Task<ServicesCartDTO> IssueNewServiceCart(List<AppointmentClass> List);
    }
}
