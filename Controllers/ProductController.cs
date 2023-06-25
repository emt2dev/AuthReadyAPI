using AuthReadyAPI.DataLayer.DTOs.Company;
using AuthReadyAPI.DataLayer.DTOs.Pagination;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace AuthReadyAPI.Controllers
{
    [Route("api/company-{companyId}")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ICompany _company;
        private readonly IUser _user;
        private readonly IProduct _product;
        private readonly ICart _cart;
        private readonly IOrder _order;

        private readonly ILogger<AuthController> _LOGS;
        private readonly IAuthManager _IAM;
        private readonly IMapper _mapper;

        private readonly UserManager<APIUser> _UM;

        public ProductController(ICompany company, IUser user, IProduct product, ICart cart, IOrder order, ILogger<AuthController> LOGS, IAuthManager IAM, IMapper mapper, UserManager<APIUser> UM)
        {
            this._company = company;
            this._LOGS = LOGS;
            this._IAM = IAM;
            this._mapper = mapper;
            this._UM = UM;
            this._product = product;
            this._cart = cart;
            this._order = order;
            this._user = user;

        }

        [HttpPost]
        [Route("[controller]/new")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<Full__Product> PRODUCTS__CREATE([FromBody] Full__Product DTO)
        {
            Full__Product givenProduct = await _product.CREATE__PRODUCT(DTO);

            return givenProduct;
        }

        /* api/Products
         */
        [HttpGet]
        [Route("[controller]/all")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<PagedResult<Base__Product>> PRODUCT__ALL(int companyId, [FromQuery] QueryParameters QP)
        {
            PagedResult<Base__Product> AllCompanyProducts = await _product.GET__PRODUCT__ALL(companyId, QP);
            return AllCompanyProducts;
        }

        /* api/Products
         * semantics, by not needed since this is going to be used by one company.
         * Can be add other companies to this.
         */
        [HttpGet]
        [Route("[controller]/{productId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<Full__Product> PRODUCT__ONE(int companyId, int productId)
        {
            Full__Product SpecificProduct = await _product.GET__PRODUCT__ONE(productId);
            return SpecificProduct;
        }
    }
}
