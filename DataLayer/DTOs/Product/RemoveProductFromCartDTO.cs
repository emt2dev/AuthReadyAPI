namespace AuthReadyAPI.DataLayer.DTOs.Product
{
    public class RemoveProductFromCartDTO
    {
        public int CartItemId { get; set; }
        public int QuantityReduce { get; set; }
        public int CartId { get; set; }
    }
}
