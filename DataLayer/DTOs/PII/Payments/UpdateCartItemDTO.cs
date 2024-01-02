namespace AuthReadyAPI.DataLayer.DTOs.PII.Payments
{
    public class UpdateCartItemDTO
    {
        public int CartId { get; set; }
        public int CartItemId { get; set; }
        public int NewQuantity { get; set; }
    }
}
