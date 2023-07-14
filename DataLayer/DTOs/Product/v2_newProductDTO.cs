namespace AuthReadyAPI.DataLayer.DTOs.Product
{
    public class v2_newProductDTO
    {
        public int companyId { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public int dollars { get; set; }
        public int cents { get; set; }
        public int quantity = 1;
    }
}