using AuthReadyAPI.DataLayer.Models;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface IV2_Product : IV2_GenericRepository<v2_ProductStripe>
    {
        public Task<IList<v2_ProductStripe>> getAllCompanyProducts(int companyId);
    }
}