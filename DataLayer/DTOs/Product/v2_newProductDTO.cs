namespace AuthReadyAPI.DataLayer.DTOs.Product
{
    public class v2_newProductDTO
    {
        public int companyId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double default_price { get; set; }
        public int quantity = 1;
        public IFormFile newImage { get; set; }
    }
}