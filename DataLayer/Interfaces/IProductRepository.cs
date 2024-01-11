using AuthReadyAPI.DataLayer.DTOs.PII.Payments;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Models.ProductInfo;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface IProductRepository
    {
        public Task<List<ProductDTO>> GetAPIProducts();
        public Task<List<ProductDTO>> GetAPIProductsByCategoryId(int CategoryId);
        public Task<List<ProductDTO>> GetAPIProductsByKeyword(string Keyword);
        public Task<List<ProductDTO>> GetCompanyProducts(int CompanyId);
        public Task<List<string>> GetAllCompanyCategories(int CompanyId);
        public Task<List<ProductDTO>> GetCompanyProductsByKeyword(string Keyword, int CompanyId);
        public Task<ProductWithStyleDTO> GetProduct(int ProductId);
        public Task<List<ProductDTO>> GetProductCartCount();
        public Task<List<ProductDTO>> GetProductOrderCount();
        public Task<List<ProductDTO>> GetProductGrossIncome();
        public Task<List<ProductDTO>> GetProductViewCount();
        public Task<List<ProductDTO>> GetProductCartCount(int CompanyId);
        public Task<List<ProductDTO>> GetProductOrderCount(int CompanyId);
        public Task<List<ProductDTO>> GetProductGrossIncome(int CompanyId);
        public Task<List<ProductDTO>> GetProductViewCount(int CompanyId);
        public Task<List<ProductWithStyleDTO>> GetAllAvailableAPIProducts();
        public Task<List<ProductWithStyleDTO>> GetAllAvailableCompanyProducts(int CompanyId);
        public Task<List<ProductWithStyleDTO>> GetAllunavailableAPIProducts();
        public Task<List<ProductWithStyleDTO>> GetAllUnavailableCompanyProducts(int CompanyId);
        public Task<ProductWithStyleDTO> GetAvailableProductStyles(int ProductId);
        public Task<List<ProductUpsellItemDTO>> GetAllUpsellItems();
        public Task<List<SingleProductCartDTO>> GetUserSingles(string UserId);
        public Task<List<SingleProductCartDTO>> GetCompanySingles(int CompanyId);

        /* Product Life Cycle
         * 1. Create Category
         * 2. Create Products for that category
         * 3. Create Styles for that product
         * 4. Add Images to the style
         * 5. Set Style available to true
         */
        public Task<bool> NewCategory(NewCategoryDTO IncomingDTO);
        public Task<bool> NewProduct(NewProductDTO IncomingDTO);
        public Task<bool> NewStyle(NewStyleDTO IncomingDTO);
        public Task<bool> NewProductImage(NewProductImageDTO IncomingDTO);
        public Task<bool> SetStyleToAvailable(int StyleId); 
        public Task<bool> SetStyleToUnavailable(int StyleId);
        public Task<bool> UpdateProduct(ProductDTO IncomingDTO);
        public Task<bool> UpdateCategory(CategoryDTO IncomingDTO);
        public Task<bool> UpdateStyle(StyleDTO IncomingDTO);
    }
}
