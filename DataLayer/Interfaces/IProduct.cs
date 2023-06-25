using AuthReadyAPI.DataLayer.DTOs.Pagination;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Models;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface IProduct : IGenericRepository<Product>
    {
        public Task<Full__Product> CREATE__PRODUCT(Full__Product DTO);
        public Task<PagedResult<Base__Product>> GET__PRODUCT__ALL(int companyId, QueryParameters QP);
        public Task<Full__Product> GET__PRODUCT__ONE(int productId);
    }
}
