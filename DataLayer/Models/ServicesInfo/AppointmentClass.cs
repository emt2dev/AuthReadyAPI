using AuthReadyAPI.DataLayer.DTOs.Services;
using NuGet.Protocol;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.Models.ServicesInfo
{
    public class AppointmentClass
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

        public AppointmentClass()
        {
            
        }

        public AppointmentClass(NewAppointmentDTO DTO)
        {
            Id = 0;
            DateOfAppointment = DateTime.ParseExact(DTO.AppointmentDateTime, "yyyy-MM-ddTHH:mm:ss", null);
            CustomerShowed = false;
            CustomerPhone = DTO.CustomerPhone;
            CustomerName = DTO.CustomerName;
            CustomerEmail = DTO.CustomerEmail;
            CompanyId = DTO.CompanyId;
            Services = new List<ServicesClass>();
            Products = new List<ServiceProductClass>();
        }

        public void AddService(ServicesClass Obj)
        {
            Services.Add(Obj);
        }

        public void AddServices(List<ServicesClass> List)
        {
            Services.AddRange(List);
        }

        public void AddProduct(ServiceProductClass Obj)
        {
            Products.Add(Obj);
        }

        public void AddProducts(List<ServiceProductClass> List)
        {
            Products.AddRange(List);
        }
    }
}
