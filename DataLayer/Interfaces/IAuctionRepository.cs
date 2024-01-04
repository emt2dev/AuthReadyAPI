using AuthReadyAPI.DataLayer.DTOs.PII.APIUser;
using AuthReadyAPI.DataLayer.DTOs.PII.Payments;
using AuthReadyAPI.DataLayer.DTOs.Product;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface IAuctionRepository
    {
        public Task<bool> AddAuctionProduct(NewAuctionProductDTO IncomingDTO);
        public Task<bool> AddBid(BidDTO IncomingDTO);
        public Task<List<AuctionProductDTO>> GetActiveAuctionProducts();
        public Task<List<AuctionProductDTO>> GetInactiveAuctionProducts();
        public Task<List<AuctionProductCartDTO>> GetFinishedAuctionsByCompanyId(int CompanyId);
        public Task<List<AuctionProductCartDTO>> GetFinishAuctionsByUserId(string UserId);
    }
}
