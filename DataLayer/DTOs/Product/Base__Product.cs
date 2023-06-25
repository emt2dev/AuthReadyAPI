namespace AuthReadyAPI.DataLayer.DTOs.Product
{
    public class Base__Product
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public double Price_Normal { get; set; }
        public double Price_Sale { get; set; }
        public string ImageURL { get; set; }
        public string CompanyId { get; set; }
    }
}
