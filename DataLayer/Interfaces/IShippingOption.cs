using AuthReadyAPI.DataLayer.Models;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface IShippingOption : IGenericRepository<ShippingOption>
    {
        Task<IList<ShippingOption>> GetAllByDimensions(double CartAreaInInches);
        Task<IList<ShippingOption>> GetAllByWeight(double CartWeight);
    }
}
