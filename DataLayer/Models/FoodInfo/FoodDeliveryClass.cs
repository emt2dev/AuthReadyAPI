namespace AuthReadyAPI.DataLayer.Models.FoodInfo
{
    public class FoodDeliveryClass
    {
        public int Id { get; set; }
        public DateTime TimeSubmitted { get; set; }
        public DateTime TimeTouched { get; set; }
        public DateTime TimeDelivered { get; set; }
        public string DriverName { get; set; }
        public string DriverLongitude { get; set; }
        public string DriverLatitude { get; set; }
        public string DestinationLatitude { get; set; }
        public string DestinationLongitude { get; set; }
        public string DestinationAddress { get; set; }
    }
}
