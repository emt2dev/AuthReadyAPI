using AuthReadyAPI.DataLayer.Models;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface ICart : IGenericRepository<Cart>
    {
        public Task<Cart> GET__EXISTING__CART(int companyId, string userId);
    }
}
