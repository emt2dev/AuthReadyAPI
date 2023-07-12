using AuthReadyAPI.DataLayer.DTOs.Order;
using AuthReadyAPI.DataLayer.DTOs.Pagination;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System.ComponentModel.Design;
using System.Text.Json;

namespace AuthReadyAPI.Controllers
{
    [Route("api/v{version:apiVersion}/orders")]
    [ApiVersion("2.0")]
    [ApiController]
    public class v2_OrderController : ControllerBase
    {
        private readonly ILogger<v2_EntryController> _LOGS;
        private readonly IMapper _mapper;
        private readonly IV2_AuthManager _IAM;
        private readonly v2_CustomerStripe? _customer;
        private readonly v2_Staff? _staff;
        private readonly IStripeService _ss;
        private readonly IV2_ShoppingCart _cart;
        private readonly IV2_Order _order;

        public v2_OrderController(ILogger<v2_EntryController> LOGS, IMapper mapper, IV2_AuthManager IAM, IV2_Order order, IStripeService ss, IV2_ShoppingCart cart)
        {
            this._LOGS = LOGS;
            this._mapper = mapper;
            this._IAM = IAM;
            this._cart = cart;
            this._ss = ss;
            this._order = order;
        }

        [HttpPost]
        [Route("submit/delivery/{companyId}/{customerId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay

        public async Task<JsonResult> newDeliveryOrder([FromRoute] int companyId, string customerId)
        {
            v2_ShoppingCart cartSubmitted = await _cart.getExistingShoppingCart(companyId, customerId);
            v2_CustomerStripe customerFound = await _IAM.getCustomerDetails(customerId);
            var sessionId = await _ss.v2_CheckOut(cartSubmitted, customerFound);

            v2_Order newOrder = new v2_Order {
                cart = cartSubmitted,
                delivery = true,
                pickedUpByCustomer = false,
                orderCompleted = false,
            };

            _ = await _order.AddAsync(newOrder);
            
            cartSubmitted.submitted = true;
            await _cart.UpdateAsync(cartSubmitted);

            v2_ShoppingCart newCart = new v2_ShoppingCart {
                customerId = cartSubmitted.customerId,
                companyId = cartSubmitted.companyId,
                cost = 0.00,
                submitted = false,
                abandoned = false,
                costInString = cartSubmitted.cost.ToString("0.##"),
            };

            await _cart.AddAsync(newCart);

            var i = new JsonResult(sessionId, new JsonSerializerOptions { PropertyNamingPolicy = null});
            return i;
        }

        [HttpPost]
        [Route("submit/takeout/{companyId}/{customerId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay

        public async Task<JsonResult> newTakeoutOrder([FromRoute] int companyId, string customerId)
        {
            v2_ShoppingCart cartSubmitted = await _cart.getExistingShoppingCart(companyId, customerId);
            v2_CustomerStripe customerFound = await _IAM.getCustomerDetails(customerId);
            var sessionId = await _ss.v2_CheckOut(cartSubmitted, customerFound);

            v2_Order newOrder = new v2_Order {
                cart = cartSubmitted,
                delivery = false,
                pickedUpByCustomer = false,
                orderCompleted = false,
            };

            _ = await _order.AddAsync(newOrder);
            
                        
            cartSubmitted.submitted = true;
            await _cart.UpdateAsync(cartSubmitted);

            v2_ShoppingCart newCart = new v2_ShoppingCart {
                customerId = cartSubmitted.customerId,
                companyId = cartSubmitted.companyId,
                cost = 0.00,
                submitted = false,
                abandoned = false,
                costInString = cartSubmitted.cost.ToString("0.##"),
            };

            await _cart.AddAsync(newCart);
            
            var i = new JsonResult(sessionId, new JsonSerializerOptions { PropertyNamingPolicy = null});
            return i;
        }

        [HttpGet]
        [Route("all/{companyId}/{customerId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay

        public async Task<IList<v2_Order>> getAllCustomerOrders([FromRoute] int companyId, string customerId)
        {
            IList<v2_Order> allOrdersList = await _order.getAllCustomerOrders(companyId, customerId);
            return allOrdersList;
        }

        [HttpGet]
        [Route("active/{companyId}/{customerId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay

        public async Task<IList<v2_Order>> getActiveCustomerOrders([FromRoute] int companyId, string customerId)
        {
            IList<v2_Order> allOrdersList = await _order.getActiveCustomerOrders(companyId, customerId);
            return allOrdersList;
        }

        [HttpGet]
        [Route("completed/{companyId}/{customerId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay

        public async Task<IList<v2_Order>> getCompletedCustomerOrders([FromRoute] int companyId, string customerId)
        {
            IList<v2_Order> allOrdersList = await _order.getCompletedCustomerOrders(companyId, customerId);
            return allOrdersList;
        }

        [HttpGet]
        [Route("company/completed/{companyId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay

        public async Task<IList<v2_Order>> getCompletedCompanyOrders([FromRoute] int companyId)
        {
            IList<v2_Order> allOrdersList = await _order.getCompletedCompanyOrders(companyId);
            return allOrdersList;
        }

        [HttpGet]
        [Route("company/active/{companyId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay

        public async Task<IList<v2_Order>> getActiveCompanyOrders([FromRoute] int companyId)
        {
            IList<v2_Order> allOrdersList = await _order.getActiveCompanyOrders(companyId);
            return allOrdersList;
        }

        [HttpGet]
        [Route("company/all/{companyId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay

        public async Task<IList<v2_Order>> getAllCompanyOrders([FromRoute] int companyId)
        {
            IList<v2_Order> allOrdersList = await _order.getAllCompanyOrders(companyId);
            return allOrdersList;
        }
    }
}