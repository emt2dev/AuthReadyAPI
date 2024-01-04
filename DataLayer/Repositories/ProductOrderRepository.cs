using AuthReadyAPI.DataLayer.DTOs.PII.APIUser;
using AuthReadyAPI.DataLayer.Interfaces;

namespace AuthReadyAPI.DataLayer.Repositories
{
    public class ProductOrderRepository : IProductOrderRepository
    {
        public Task<bool> SubmitAuctionOrder(string UserId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SubmitBid(BidDTO DTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SubmitCartOrder(string UserId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SubmitSingleOrder(string UserId)
        {
            throw new NotImplementedException();
        }
    }
}
