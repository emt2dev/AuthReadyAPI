using AuthReadyAPI.DataLayer.DTOs.Pagination;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Models;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface IProduct : IGenericRepository<Product>
    {
        Task<IList<Product>> GET__PRODUCT__ALL(int companyId);
        public Task<PagedResult<Product>> GET__PRODUCT__KEYWORD__ALL(int companyId, QueryParameters QP, string keyword);
        public Task<Product> GET__PRODUCT__ONE(int productId);
    }
}
