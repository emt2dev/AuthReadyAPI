using AuthReadyAPI.DataLayer.DTOs.APIUser;
using AuthReadyAPI.DataLayer.Models;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface IStripeService
    {
        public Task<string> v2_CheckOut(v2_ShoppingCart cart, v2_CustomerDTO customer);
    }
}
