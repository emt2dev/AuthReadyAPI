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
        private readonly IProduct _product;

        public ProductController(IProduct product)
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
        public async Task<List<ProductDTO>> GetAPIByCategory([FromForm] int CategoryId)
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
        
        [HttpPost]
        [Route("list/company")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ProductDTO>> GetCompanyProducts([FromForm] int CompanyId)
        {
            return await _product.GetCompanyProducts(CompanyId);
        }
        
        [HttpPost]
        [Route("list/company/{CategoryName}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ProductDTO>> GetCompanyProductsByCat([FromForm] int CompanyId, [FromRoute] string CategoryName)
        {
            // todo: create a dto due to the way FromForm works
            return await _product.GetCompanyProductsByCategoryName(CategoryName, CompanyId);
        }
        
        [HttpPost]
        [Route("list/company/keyword/{keyword}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ProductDTO>> GetCompanyProductsByKeyword([FromForm] int CompanyId, [FromRoute] string Keyword)
        {
            return await _product.GetCompanyProductsByKeyword(Keyword, CompanyId);
        }
                
        [HttpPost]
        [Route("details/one/{ProductId}")]
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
        public async Task<List<ProductDTO>> GetCompanyCartCount([FromForm] int CompanyId)
        {
            return await _product.GetProductCartCount(CompanyId);
        }

        [HttpPost]
        [Route("company/count/order")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ProductDTO>> GetCompanyOrderCount([FromForm] int CompanyId)
        {
            return await _product.GetProductOrderCount(CompanyId);
        }

        [HttpPost]
        [Route("company/count/income")]
        [Authorize(Roles = "Company")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ProductDTO>> GetCompanyGrossIncomeCount([FromForm] int CompanyId)
        {
            return await _product.GetProductGrossIncome(CompanyId);
        }

        [HttpPost]
        [Route("company/count/views")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ProductDTO>> GetCompanyViewCount([FromForm] int CompanyId)
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

        [HttpPost]
        [Route("list/available/company")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ProductWithStyleDTO>> GetCompanyAvailable([FromForm] int CompanyId)
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
        [Authorize(Roles = "Company")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ProductWithStyleDTO>> GetCompanyUnavailable([FromForm] int CompanyId)
        {
            return await _product.GetAllUnavailableCompanyProducts(CompanyId);
        }
    }
}
