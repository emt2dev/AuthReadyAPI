using AuthReadyAPI.DataLayer.Models.PII;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface IOrderRepository
    {
        public Task<string> PrepareCart(string CartType, int CartId);
        public Task<bool> FinalizeSale(int PreparedCartId);
    }
}
