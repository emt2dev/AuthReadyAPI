namespace AuthReadyAPI.DataLayer.DTOs.Services
{
    public class AppointmentSpecificsDTO
    {
        public string AppointmentDateTime { get; set; }
        public List<int> ServicesClassIds { get; set; }
        public List<int> ServicesProductIds { get; set; }
    }
}
