using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthReadyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _product;

        public ProductController(IProductRepository product)
        {
            _product = product;
        }

        /*
         * This controller is to display products, their metrics, and their styles
         * Products are maniupulated in the company controller
         */

        [HttpGet]
        [Route("list/all")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ProductDTO>> GetAllAPI()
        {
            return await _product.GetAPIProducts();
        }
                
        [HttpPost]
        [Route("list/category")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ProductDTO>> GetAPIByCategory([FromBody] int CategoryId)
        {
            return await _product.GetAPIProductsByCategoryId(CategoryId);
        }
                
        [HttpGet]
        [Route("list/keyword/{keyword}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ProductDTO>> GetAPIBykeyword([FromRoute] string Keyword)
        {
            return await _product.GetAPIProductsByKeyword(Keyword);
        }
        
        [HttpGet]
        [Route("list/company/{CompanyId}/all")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ProductDTO>> GetCompanyProducts([FromRoute] int CompanyId)
        {
            return await _product.GetCompanyProducts(CompanyId);
        }
        [HttpGet]
        [Route("list/company/{CompanyId}/categories/all")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<string>> GetCompanyCats([FromRoute] int CompanyId)
        {
            // todo: create a dto due to the way FromBody works
            return await _product.GetAllCompanyCategories(CompanyId);
        }

        [HttpPost]
        [Route("list/company/{CompanyId}/keyword/{keyword}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ProductDTO>> GetCompanyProductsByKeyword([FromRoute] int CompanyId, [FromRoute] string Keyword)
        {
            return await _product.GetCompanyProductsByKeyword(Keyword, CompanyId);
        }
                
        [HttpGet]
        [Route("details/{ProductId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ProductWithStyleDTO> GetProductById([FromRoute] int ProductId)
        {
            // Need to add DTO to include all styles
            return await _product.GetProduct(ProductId);
        }
        
        [HttpGet]
        [Route("api/count/cart")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ProductDTO>> GetAPICartCount()
        {
            return await _product.GetProductCartCount();
        }
        
        [HttpGet]
        [Route("api/count/order")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ProductDTO>> GetAPIOrderCount()
        {
            return await _product.GetProductOrderCount();
        }
        
        [HttpGet]
        [Route("api/count/income")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ProductDTO>> GetAPIGrossIncomeCount()
        {
            return await _product.GetProductGrossIncome();
        }

        [HttpPost]
        [Route("api/count/views")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ProductDTO>> GetAPIViewCount()
        {
            return await _product.GetProductViewCount();
        }


        [HttpPost]
        [Route("company/count/cart")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ProductDTO>> GetCompanyCartCount([FromBody] int CompanyId)
        {
            return await _product.GetProductCartCount(CompanyId);
        }

        [HttpPost]
        [Route("count/order/{CompanyId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ProductDTO>> GetCompanyOrderCount([FromRoute] int CompanyId)
        {
            return await _product.GetProductOrderCount(CompanyId);
        }

        [HttpPost]
        [Route("count/income/{CompanyId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ProductDTO>> GetCompanyGrossIncomeCount([FromRoute] int CompanyId)
        {
            return await _product.GetProductGrossIncome(CompanyId);
        }

        [HttpPost]
        [Route("count/views/{CompanyId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ProductDTO>> GetCompanyViewCount([FromRoute] int CompanyId)
        {
            return await _product.GetProductViewCount(CompanyId);
        }

        [HttpGet]
        [Route("list/available/api")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ProductWithStyleDTO>> GetAPIAvailable()
        {
            return await _product.GetAllAvailableAPIProducts();
        }

        [HttpGet]
        [Route("list/available/company/{CompanyId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ProductWithStyleDTO>> GetCompanyAvailable([FromRoute] int CompanyId)
        {
            return await _product.GetAllAvailableCompanyProducts(CompanyId);
        }

        [HttpGet]
        [Route("list/unavailable/api")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ProductWithStyleDTO>> GetAPIUnavailable()
        {
            return await _product.GetAllunavailableAPIProducts();
        }

        [HttpPost]
        [Route("list/unavailable/company")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ProductWithStyleDTO>> GetCompanyUnavailable([FromBody] int CompanyId)
        {
            return await _product.GetAllUnavailableCompanyProducts(CompanyId);
        }

    }
}
