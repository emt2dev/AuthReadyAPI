using AuthReadyAPI.DataLayer.DTOs.Order;
using AuthReadyAPI.DataLayer.DTOs.Pagination;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.ComponentModel.Design;

namespace AuthReadyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ICompany _company;
        private readonly IUser _user;
        private readonly IProduct _product;
        private readonly IShoppingCart _cart;
        private readonly IOrder _order;

        private readonly ILogger<AuthController> _LOGS;
        private readonly IAuthManager _IAM;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configs;
        private readonly UserManager<APIUser> _UM;

        public OrderController(IConfiguration configs, ICompany company, IUser user, IProduct product, IShoppingCart cart, IOrder order, ILogger<AuthController> LOGS, IAuthManager IAM, IMapper mapper, UserManager<APIUser> UM)
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
            this._configs = configs;
        }

        /* api/order/submit/pickup */
        [HttpPost]
        [Route("submit/pickup/{companyId}/{customerId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IActionResult> ORDER__SUBMITTED([FromRoute] int companyId, string customerId)
        {
            /* 
                * Process stripe api payment here
                * Notify Company of new order
                * Notify customer of payment processed
            */
            // shoppingCart cartSubmitted = await _cart.GetAsyncById(shoppingCartId);
            shoppingCart cartSubmitted = await _cart.GET__EXISTING__CART(companyId, customerId);
            APIUser customerFound = await _IAM.USER__DETAILS(customerId);

            var options = new SessionCreateOptions {
                PaymentMethodTypes = new List<string>
                    {
                        "card",
                    },
                    AutomaticTax = new SessionAutomaticTaxOptions { Enabled = true },
                    LineItems = new List<SessionLineItemOptions>(),
                    Currency = "usd",
                    Mode = "payment",
                    SuccessUrl = "http://localhost:4200/",
                    CancelUrl = "http://localhost:4200/",
                    CustomerEmail = customerFound.Email,
            };

            // Populate LineItems with detail from each ShoppingCart
            foreach (CartItem item in cartSubmitted.Items)
            {
                SessionLineItemOptions sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {

                        TaxBehavior = "inclusive",
                        UnitAmount = (long)(item.price * 100),//20.00 -> 2000
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Description = item.description,
                            Name = item.name,
                            TaxCode = "txcd_40060003",
                        },
                    },
                    
                    Quantity = Convert.ToInt64(item.count),
                };
                options.LineItems.Add(sessionLineItem);
            }
            
            //  return Ok(options.LineItems);

            // Create a Stripe API session service
            var service = new SessionService();

            // return Ok(service);
            // Use the session service to create a Stripe API session
            
            Session session = service.Create(options);
            return Ok(session);
           

            // return Ok("reached");
        }

        /* api/order/cancel */
        [HttpPost]
        [Route("cancel")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public string ORDER__CANCEL(int cartId)
        {
            /*
             * 
             * Process refund here
             * Notify customer here
             * 
             */ 
            string result = $"cartId: {cartId}, return order currentstatus == cancelled";
            return result;
        }

        /* api/order/update */
        [HttpPut]
        [Route("update")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public string ORDER__UPDATE(Full__Order DTO)
        {
            /*
             * 
             * Notify customer of order status change
             * 
             */ 
            string result = $"order DTO, return updated order (ie: currentstatus == cooking/preparing/delivering/delivered/etc)";
            return result;
        }
    }
}
