using AuthReadyAPI.DataLayer.DTOs.PII.Payments;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Models.PII;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface ICartRepository
    {
        public Task<ShoppingCartDTO> GetUserCart(string UserId);
        public Task<bool> AddItem(AddProductToCartDTO IncomingDTO);
        public Task<bool> RemoveItem(RemoveProductFromCartDTO IncomingDTO);
        public Task<ShoppingCartDTO> IssueNewCart(int CartId);
        public Task<ShoppingCartDTO> UpdateCart(ShoppingCartDTO IncomingDTO);
        public Task<bool> AddSingleProductCart(NewSingleProductDTO IncomingDTO);
        public Task<List<SingleProductCartDTO>> GetSingleProductCarts(string UserId);
        public Task<List<AuctionProductCartDTO>> GetAuctionProductCarts(string UserId);
    }
}
