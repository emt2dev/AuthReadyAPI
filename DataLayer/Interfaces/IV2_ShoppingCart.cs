using AuthReadyAPI.DataLayer.Models;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface IV2_ShoppingCart : IV2_GenericRepository<v2_ShoppingCart>
    {
        public Task<v2_ShoppingCart> getCart(int cartId);
        public Task<v2_ShoppingCart> getExistingShoppingCart(int companyId, string customerId);
    }
}
