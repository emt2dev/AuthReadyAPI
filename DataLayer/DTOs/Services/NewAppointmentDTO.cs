namespace AuthReadyAPI.DataLayer.DTOs.Services
{
    public class NewAppointmentDTO
    {
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerName { get; set; }
        public List<AppointmentSpecificsDTO> Specifics { get; set; }
        public int CompanyId { get; set; }
    }
}
