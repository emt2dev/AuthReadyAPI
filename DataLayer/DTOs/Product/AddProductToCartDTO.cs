namespace AuthReadyAPI.DataLayer.DTOs.Product
{
    public class AddProductToCartDTO
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int StyleId { get; set; }
        public int Count { get; set; }
    }
}
