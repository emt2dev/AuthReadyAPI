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
        [Route("submit/pickup")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IActionResult> ORDER__SUBMITTED([FromBody] int shoppingCartId)
        {
            /* 
                * Process stripe api payment here
                * Notify Company of new order
                * Notify customer of payment processed
            */
            shoppingCart cartSubmitted = await _cart.GetAsyncById(shoppingCartId);
            List<SessionLineItemOptions> listTransfer = new List<SessionLineItemOptions>();

            // Populate LineItems with detail from each ShoppingCart
            foreach (var item in cartSubmitted.Items)
            {
                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.price * 100),//20.00 -> 2000
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.name
                        },
                    },
                    Quantity = item.count,
                };
                listTransfer.Add(sessionLineItem);
            }

            var options = new SessionCreateOptions {
                PaymentMethodTypes = new List<string>
                    {
                        "card",
                    },
                    LineItems = listTransfer,
                    Currency = "usd",
                    Mode = "payment",
                    SuccessUrl = "order received",
                    CancelUrl = "payment failed",
            };

            // Populate LineItems with detail from each ShoppingCart
            // foreach (var item in cartSubmitted.Items)
            // {
            //     var sessionLineItem = new SessionLineItemOptions
            //     {
            //         PriceData = new SessionLineItemPriceDataOptions
            //         {
            //             UnitAmount = (long)(item.price * 100),//20.00 -> 2000
            //             Currency = "usd",
            //             ProductData = new SessionLineItemPriceDataProductDataOptions
            //             {
            //                 Name = item.name
            //             },
            //         },
            //         Quantity = item.count,
            //     };
            //     options.LineItems.Add(sessionLineItem);
            // }

            return Ok(options);

            // // Create a Stripe API session service
            // var service = new SessionService();
            
            // // Use the session service to create a Stripe API session
            // Session session = service.Create(options);

            

            // if(session.Url == "order received") {
            //     Order newOrder = new Order {
            //         shoppingCartId = shoppingCartId.ToString(),
            //         CurrentStatus = "Order recevied",
            //         Payment_Complete = true,
            //         delivery = false,
            //         Payment_Amount = cartSubmitted.cost,
            //         Time__Submitted = DateTime.Now
            //     };
            // } else {
            //     return Ok(options);
            //     // return "payment failed";
            // }

            // string result = $"shoppingCartId: {shoppingCartId}, return order";
            // return Ok(result);
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
