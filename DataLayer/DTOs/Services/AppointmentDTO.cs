using AuthReadyAPI.DataLayer.Models.ServicesInfo;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.DTOs.Services
{
    public class AppointmentDTO
    {
        public int Id { get; set; }
        public DateTime DateOfAppointment { get; set; }
        public List<ServicesClass> Services { get; set; }
        public List<ServiceProductClass> Products { get; set; }
        public bool CustomerShowed { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerName { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public int CompanyId { get; set; }
    }
}
