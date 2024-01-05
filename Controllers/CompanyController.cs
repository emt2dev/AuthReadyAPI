using AuthReadyAPI.DataLayer.DTOs.Company;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models.Companies;
using AuthReadyAPI.DataLayer.Models.PII;
using AuthReadyAPI.DataLayer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthReadyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _company;
        private readonly IProductRepository _product;
        private readonly IAuthRepository _authRepository;
        private readonly UserManager<APIUserClass> _userManager;
        APIUserClass _user;

        public CompanyController(ICompanyRepository company, IProductRepository product, UserManager<APIUserClass> userManager, IAuthRepository authRepository)
        {
            _company = company;
            _product = product;
            _authRepository = authRepository;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("debug/company")]
        [Authorize(Roles = ("Admin"))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<bool> NewCompany([FromForm] NewCompanyDTO IncomingDTO)
        {
            string Token = (string)HttpContext.Request.Headers["Authorization"];
            Token = Token.Replace("Bearer ", "");

            _user = await _userManager.FindByIdAsync(await _authRepository.ReadUserId(Token));
            if (_user is null) return false;

            return await _company.NewCompany(IncomingDTO);
        }

        [HttpPost]
        [Route("new/contact")]
        [Authorize(Roles = ("Company,Admin"))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<bool> NewContact([FromForm] NewPointOfContactDTO IncomingDTO)
        {
            string Token = (string)HttpContext.Request.Headers["Authorization"];
            Token = Token.Replace("Bearer ", "");

            _user = await _userManager.FindByIdAsync(await _authRepository.ReadUserId(Token));
            if (_user is null) return false;

            return await _company.NewContact(IncomingDTO);
        }

        [HttpPost]
        [Route("debug/all/company")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<List<CompanyDTO>> GetAllCompanies()
        {
            List<CompanyDTO> CompanyList = await _company.GetAPICompanyList();
            return CompanyList;
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
        [Authorize(Roles = ("Admin,Company"))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<bool> NewProductCategory([FromForm] NewCategoryDTO IncomingDTO)
        {
            string Token = (string)HttpContext.Request.Headers["Authorization"];
            Token = Token.Replace("Bearer ", "");

            _user = await _userManager.FindByIdAsync(await _authRepository.ReadUserId(Token));
            if (_user is null) return false;

            return await _product.NewCategory(IncomingDTO);
        }

        [HttpPost]
        [Route("new/product")]
        [Authorize(Roles = ("Company"))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<bool> NewProduct([FromForm] NewProductDTO IncomingDTO)
        {
            string Token = (string)HttpContext.Request.Headers["Authorization"];
            Token = Token.Replace("Bearer ", "");

            _user = await _userManager.FindByIdAsync(await _authRepository.ReadUserId(Token));
            if (_user is null) return false;

            return await _product.NewProduct(IncomingDTO);
        }

        [HttpPost]
        [Route("new/style")]
        [Authorize(Roles = ("Company"))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<bool> NewStyle([FromForm] NewStyleDTO IncomingDTO)
        {
            string Token = (string)HttpContext.Request.Headers["Authorization"];
            Token = Token.Replace("Bearer ", "");

            _user = await _userManager.FindByIdAsync(await _authRepository.ReadUserId(Token));
            if (_user is null) return false;

            return await _product.NewStyle(IncomingDTO);
        }

        [HttpPost]
        [Route("image/product/new")]
        [Authorize(Roles = ("Company"))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<bool> NewProductImage([FromForm] NewProductImageDTO IncomingDTO)
        {
            string Token = (string)HttpContext.Request.Headers["Authorization"];
            Token = Token.Replace("Bearer ", "");

            _user = await _userManager.FindByIdAsync(await _authRepository.ReadUserId(Token));
            if (_user is null) return false;

            return await _product.NewProductImage(IncomingDTO);
        }

        [HttpPost]
        [Route("style/available")]
        [Authorize(Roles = ("Company"))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<bool> StyleIsAvailable([FromForm] int StyleId)
        {
            string Token = (string)HttpContext.Request.Headers["Authorization"];
            Token = Token.Replace("Bearer ", "");

            _user = await _userManager.FindByIdAsync(await _authRepository.ReadUserId(Token));
            if (_user is null) return false;

            return await _product.SetStyleToAvailable(StyleId);
        }

        [HttpPost]
        [Route("style/unavailable")]
        [Authorize(Roles = ("Company"))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<bool> StyleNotAvailable([FromForm] int StyleId)
        {
            string Token = (string)HttpContext.Request.Headers["Authorization"];
            Token = Token.Replace("Bearer ", "");

            _user = await _userManager.FindByIdAsync(await _authRepository.ReadUserId(Token));
            if (_user is null) return false;

            return await _product.SetStyleToUnavailable(StyleId);
        }
    }
}
