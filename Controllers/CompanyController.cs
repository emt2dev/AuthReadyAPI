using AuthReadyAPI.DataLayer.DTOs.Company;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthReadyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompany _company;
        private readonly IProduct _product;

        public CompanyController(ICompany company, IProduct product)
        {
            _company = company;
            _product = product;
        }

        [HttpPost]
        [Route("debug/company")]
        //[Authorize(Roles = ("API_Admin, Company_Admin"))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IActionResult> NewCompany([FromForm] NewCompanyDTO IncomingDTO)
        {
            return Ok(await _company.NewCompany(IncomingDTO));
        }

        [HttpPost]
        [Route("new/contact")]
        //[Authorize(Roles = ("API_Admin, Company_Admin"))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IActionResult> NewContact([FromForm] NewPointOfContactDTO IncomingDTO)
        {
            return Ok(await _company.NewContact(IncomingDTO));
        }

        [HttpPost]
        [Route("debug/all/company")]
        //[Authorize(Roles = ("API_Admin, Company_Admin"))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IActionResult> GetAllCompanies()
        {
            return Ok(await _company.GetAPICompanyList());
        }

        /* Product Life Cycle
         * 1. Create Category
         * 2. Create Products for that category
         * 3. Create Styles for that product
         * 4. Add Images to the style
         * 5. Set Style available to true
         */

        [HttpPost]
        [Route("new/category")]
        //[Authorize(Roles = ("API_Admin, Company_Admin"))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IActionResult> NewProductCategory([FromForm] NewCategoryDTO IncomingDTO)
        {
            return Ok(await _product.NewCategory(IncomingDTO));
        }

        [HttpPost]
        [Route("new/product")]
        //[Authorize(Roles = ("API_Admin, Company_Admin"))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IActionResult> NewProduct([FromForm] NewProductDTO IncomingDTO)
        {
            return Ok(await _product.NewProduct(IncomingDTO));
        }

        [HttpPost]
        [Route("new/style")]
        //[Authorize(Roles = ("API_Admin, Company_Admin"))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IActionResult> NewStyle([FromForm] NewStyleDTO IncomingDTO)
        {
            return Ok(await _product.NewStyle(IncomingDTO));
        }

        [HttpPost]
        [Route("image/product/new")]
        //[Authorize(Roles = ("API_Admin, Company_Admin"))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IActionResult> NewProductImage([FromForm] NewProductImageDTO IncomingDTO)
        {
            return Ok(await _product.NewProductImage(IncomingDTO));
        }

        [HttpPost]
        [Route("style/available")]
        //[Authorize(Roles = ("API_Admin, Company_Admin"))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IActionResult> StyleIsAvailable([FromForm] int StyleId)
        {
            return Ok(await _product.SetStyleToAvailable(StyleId));
        }

        [HttpPost]
        [Route("style/unavailable")]
        //[Authorize(Roles = ("API_Admin, Company_Admin"))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IActionResult> StyleNotAvailable([FromForm] int StyleId)
        {
            return Ok(await _product.SetStyleToUnavailable(StyleId));
        }
    }
}
