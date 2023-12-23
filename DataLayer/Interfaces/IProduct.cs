using AuthReadyAPI.DataLayer.DTOs.Product;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface IProduct
    {
        public Task<List<ProductDTO>> GetAPIProducts();
        public Task<List<ProductDTO>> GetAPIProductsByCategoryId(int CategoryId);
        public Task<List<ProductDTO>> GetAPIProductsByKeyword(string Keyword);
        public Task<List<ProductDTO>> GetCompanyProducts(int CompanyId);
        public Task<List<ProductDTO>> GetCompanyProductsByCategoryId(int CategoryId, int CompanyId);
        public Task<List<ProductDTO>> GetCompanyProductsByKeyword(string Keyword, int CompanyId);
        public Task<ProductDTO> GetProduct(int ProductId);
        public Task<ProductWithStyleDTO> GetProductCartCount();
        public Task<ProductWithStyleDTO> GetProductOrderCount();
        public Task<ProductWithStyleDTO> GetProductGrossIncome();
        public Task<ProductDTO> GetProductViewCount();
        public Task<ProductWithStyleDTO> GetProductCartCount(int CompanyId);
        public Task<ProductWithStyleDTO> GetProductOrderCount(int CompanyId);
        public Task<ProductWithStyleDTO> GetProductGrossIncome(int CompanyId);
        public Task<ProductDTO> GetProductViewCount(int CompanyId);
        public Task<List<ProductWithStyleDTO>> GetAllAvailableAPIProducts();
        public Task<List<ProductWithStyleDTO>> GetAllAvailableCompanyProducts(int CompanyId);
        public Task<List<ProductWithStyleDTO>> GetAllunavailableAPIProducts();
        public Task<List<ProductWithStyleDTO>> GetAllUnavailableCompanyProducts(int CompanyId);
        public Task<List<StyleDTO>> GetAvailableProductStyles(int ProductId);

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
