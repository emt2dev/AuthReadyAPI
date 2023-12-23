using AuthReadyAPI.DataLayer.DTOs.Pagination;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Models;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface IProduct : IGenericRepository<ProductDTO>
    {
        public Task<List<ProductDTO>> GetAPIProducts();
        public Task<List<ProductDTO>> GetAPIProductsByCategoryId(int CategoryId);
        public Task<List<ProductDTO>> GetAPIProductsByKeyword(string Keyword);
        public Task<List<ProductDTO>> GetCompanyProducts(int CompanyId);
        public Task<List<ProductDTO>> GetCompanyProductsByCategoryId(int CategoryId, int CompanyId);
        public Task<List<ProductDTO>> GetCompanyProductsByKeyword(string Keyword, int CompanyId);
        public Task<ProductDTO> GetProduct(int ProductId);
        public ProductDTO GetProductCartCount();
        public ProductDTO GetProductOrderCount();
        public ProductDTO GetProductGrossIncome();
        public ProductDTO GetProductCartCount(int CompanyId);
        public ProductDTO GetProductOrderCount(int CompanyId);
        public ProductDTO GetProductGrossIncome(int CompanyId);
        public Task<List<ProductDTO>> GetAllAvailableAPIProducts();
        public Task<List<ProductDTO>> GetAllAvailableCompanyProducts(int CompanyId);
    }
}
