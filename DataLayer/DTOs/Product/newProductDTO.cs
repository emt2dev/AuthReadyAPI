namespace AuthReadyAPI.DataLayer.DTOs.Product
{
    public class newProductDTO
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public double Price_Normal { get; set; }
        public double Price_Sale { get; set; }
        public IFormFile ImageURL { get; set; }
        public string CompanyId { get; set; }
        public string Keyword { get; set; }
    }
}