using AuthReadyAPI.DataLayer.DTOs.PII.APIUser;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface IProductOrder
    {
        public Task<bool> SubmitBid(BidDTO DTO);
        public Task<bool> SubmitCartOrder(string UserId);
        public Task<bool> SubmitAuctionOrder(string UserId);
        public Task<bool> SubmitSingleOrder(string UserId);
    }
}
