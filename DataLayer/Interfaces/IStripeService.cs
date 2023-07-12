using AuthReadyAPI.DataLayer.Models;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface IStripeService
    {
        // public Task<string> CheckOut(v2_ProductStripe product, long price);
        public Task<string> CheckOut(shoppingCart cart, v2_CustomerStripe customer);
        public Task<string> CheckOut(shoppingCart cart, APIUser customer);
        public Task<string> v2_CheckOut(v2_ShoppingCart cart, v2_CustomerStripe customer);
    }
}
