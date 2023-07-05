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
    [Route("api/[controller]")]
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

        // [HttpPost]
        // [Route("list/category/{keyword}/{companyId}")]
        // // ?StartIndex={StartIndex}&pagesize={pagesize}&pagenumber={pagenumber}
        // [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        // [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        // [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        // public async Task<PagedResult<Base__Product>> PRODUCT__KEYWORD__ALL(int companyId, [FromQuery] QueryParameters QP, string keyword)
        // {
        //     PagedResult<Product> AllKeywordProducts = await _product.GET__PRODUCT__KEYWORD__ALL(companyId, QP, keyword);
        //     return AllKeywordProducts;
        // }
/*
        [HttpPost]
        [Route("list/category/{keyword}/{companyId}")]
        // ?StartIndex={StartIndex}&pagesize={pagesize}&pagenumber={pagenumber}
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<PagedResult<Base__Product>> PRODUCT__KEYWORD__ALL(int companyId, [FromQuery] QueryParameters QP, string keyword)
        {
            PagedResult<Base__Product> AllKeywordProducts = await _product.GET__PRODUCT__KEYWORD__ALL(companyId, QP, keyword);
            return AllKeywordProducts;
        }
        */
        /* api/Product/all/{keyword}
 
        [HttpPost]
        [Route("list/category/{keyword}/{companyId}")]
        // ?StartIndex={StartIndex}&pagesize={pagesize}&pagenumber={pagenumber}
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<PagedResult<Base__Product>> PRODUCT__KEYWORD__ALL(int companyId, [FromQuery] QueryParameters QP, string keyword)
        {
            PagedResult<Base__Product> AllKeywordProducts = await _product.GET__PRODUCT__KEYWORD__ALL(companyId, QP, keyword);
            return AllKeywordProducts;
        }
*/

        [HttpGet]
        [Route("list/all/{companyId}")]
        // ?StartIndex={StartIndex}&pagesize={pagesize}&pagenumber={pagenumber}
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        // public async Task<IList<Full__Product>> PRODUCT__ALL(int companyId, [FromQuery] QueryParameters QP)
        public async Task<IList<Full__Product>> PRODUCT__ALL(int companyId)
        {
            IList<Product> ProductsList = new List<Product>();
            IList<Full__Product> ProductDTOs = new List<Full__Product>();

            ProductsList = await _product.GET__PRODUCT__ALL(companyId);

            foreach (var product in ProductsList)
                {
                    Full__Product mappedProduct = new Full__Product {
                        Id = product.Id.ToString(),
                        Name = product.Name,
                        Description = product.Description,
                        Price_Current = product.Price_Current,
                        Price_Sale = product.Price_Sale,
                        Price_Normal = product.Price_Normal,
                        ImageURL = product.ImageURL,
                        CompanyId = product.Company,
                        Modifiers = product.Modifiers,
                        Keyword = product.Keyword,
                    };

                    ProductDTOs.Add(mappedProduct);
                }

            return ProductDTOs;
        }
/*
        [HttpGet]
        [Route("list/all/{companyId}")]
        // ?StartIndex={StartIndex}&pagesize={pagesize}&pagenumber={pagenumber}
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<PagedResult<Base__Product>> PRODUCT__ALL(int companyId, [FromQuery] QueryParameters QP)
        {
            PagedResult<Base__Product> AllCompanyProducts = await _product.GET__PRODUCT__ALL(companyId, QP);
            return AllCompanyProducts;
        }
        */

        /* api/Product/all/{keyword}

        [HttpGet]
        [Route("list/all/{companyId}")]
        // ?StartIndex={StartIndex}&pagesize={pagesize}&pagenumber={pagenumber}
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<PagedResult<Base__Product>> PRODUCT__ALL(int companyId, [FromQuery] QueryParameters QP)
        {
            PagedResult<Base__Product> AllCompanyProducts = await _product.GET__PRODUCT__ALL(companyId, QP);
            return AllCompanyProducts;
        }
*/
        [HttpGet]
        [Route("details/{productId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public string PRODUCT__ONE([FromRoute] int productId)
        {
            return "full product details recevied";
        }
/*
        [HttpGet]
        [Route("details/{productId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<Full__Product> PRODUCT__ONE([FromRoute] int productId)
        {
            Full__Product SpecificProduct = await _product.GET__PRODUCT__ONE(productId);
            return SpecificProduct;
        }
        */
        /* api/Products
         * semantics, by not needed since this is going to be used by one company.
         * Can be add other companies to this.
         
        [HttpGet]
        [Route("details/{productId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<Full__Product> PRODUCT__ONE([FromRoute] int productId)
        {
            Full__Product SpecificProduct = await _product.GET__PRODUCT__ONE(productId);
            return SpecificProduct;
        }
        */
    }
}
