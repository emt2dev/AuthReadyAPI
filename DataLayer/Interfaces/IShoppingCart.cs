using AuthReadyAPI.DataLayer.Models;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface IShoppingCart : IGenericRepository<shoppingCart>
    {
        public  Task<shoppingCart> GET__EXISTING__CART(int companyId, string customerId);
    }
}
