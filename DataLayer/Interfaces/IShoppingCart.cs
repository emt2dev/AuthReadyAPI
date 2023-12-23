using AuthReadyAPI.DataLayer.Models.PII;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface IShoppingCart
    {
        public  Task<ShoppingCartClass> GET__EXISTING__CART(int companyId, string customerId);
    }
}
