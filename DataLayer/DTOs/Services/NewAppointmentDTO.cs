namespace AuthReadyAPI.DataLayer.DTOs.Services
{
    public class NewAppointmentDTO
    {
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerName { get; set; }
        public string AppointmentDateTime { get; set; }
        public List<int> ServicesClassIds { get; set; }
        public int CompanyId { get; set; }
    }
}
