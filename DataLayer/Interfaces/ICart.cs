using AuthReadyAPI.DataLayer.DTOs.PII.Payments;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Models.PII;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface ICart
    {
        public Task<ShoppingCartDTO> GetUserCart(string UserId);
        public Task<bool> AddItem(AddProductToCartDTO IncomingDTO);
        public Task<bool> RemoveItem(RemoveProductFromCartDTO IncomingDTO);
    }
}
