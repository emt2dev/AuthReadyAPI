using AuthReadyAPI.DataLayer.DTOs.PII.APIUser;
using AuthReadyAPI.DataLayer.DTOs.Product;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface IAuction
    {
        public Task<bool> AddAuctionProduct(NewAuctionProductDTO IncomingDTO);
        public Task<bool> AddBid(BidDTO IncomingDTO);
        public Task<List<AuctionProductDTO>> GetAuctionProducts();
        public Task<List<AuctionProductDTO>> GetFinishedAuctionsByCompanyId(int CompanyId);
        public Task<List<AuctionProductDTO>> GetFinishAuctionsByUserId(string UserId);
    }
}
