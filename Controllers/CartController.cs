using AuthReadyAPI.DataLayer.DTOs.Cart;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthReadyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
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

        public CartController(ICompany company, IUser user, IProduct product, ICart cart, IOrder order, ILogger<AuthController> LOGS, IAuthManager IAM, IMapper mapper, UserManager<APIUser> UM)
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

        // api/cart/existing/{companyId}/{customerId}
        [HttpGet]
        [Route("existing/{companyId}/{customerId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public string CART__GET__EXISTING(int companyId, int customerId)
        {
            string result = $"company: {companyId}, customer: {customerId}";
            return result;
        }

        // api/cart/new/{companyId}/{customerId}
        [HttpPost]
        [Route("new/{companyId}/{customerId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public string CART__CREATE__NEW(int companyId, int customerId)
        {
            string result = $"company: {companyId}, customer: {customerId}";
            return result;
        }

        // api/cart/add/{companyId}/{customerId}
        [HttpGet]
        [Route("add/{companyId}/{customerId}/{productId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public string CART__ADD__PRODUCT(int companyId, int customerId, int productId)
        {
            /*
             * 
             *  flowchart
             *  if no cart exists with customer and company ids and submitted != true, create cart, add product, return cart
             *  if cart == abandoned, change that to false and use that cart
             *  if cart exists add product to cart, return cart
             *
             */
            string result = $"company: {companyId}, customer: {customerId}, product: {productId}";
            return result;
        }
    }
}
