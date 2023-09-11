using AuthReadyAPI.DataLayer.Models;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface IShippedProduct : IGenericRepository<ShippedProduct>
    {
        Task<IList<ShippedProduct>> GetAllUnAvailable(int companyId);
        Task<IList<ShippedProduct>> GetAllAvailable(int companyId);
    }
}
