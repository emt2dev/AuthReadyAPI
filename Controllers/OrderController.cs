using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models.PII;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        private readonly UserManager<APIUserClass> _UM;

        public OrderController(IConfiguration configs, ICompany company, IUser user, IProduct product, IShoppingCart cart, IOrder order, ILogger<AuthController> LOGS, IAuthManager IAM, IMapper mapper, UserManager<APIUserClass> UM)
        {
            _company = company;
            _LOGS = LOGS;
            _IAM = IAM;
            _mapper = mapper;
            _UM = UM;
            _product = product;
            _cart = cart;
            _order = order;
            _user = user;
            _configs = configs;
        }
        /* 
        [HttpPost]
        [Route("submit/pickup/{companyId}/{customerId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IActionResult> ORDER__SUBMITTED([FromRoute] int companyId, string customerId)
        {
            
                * Process stripe api payment here
                * Notify Company of new order
                * Notify customer of payment processed
            
            // shoppingCart cartSubmitted = await _cart.GetAsyncById(shoppingCartId);
            ShoppingCartClass cartSubmitted = await _cart.GET__EXISTING__CART(companyId, customerId);
            APIUserClass customerFound = await _IAM.USER__DETAILS(customerId);

            var options = new SessionCreateOptions {
                PaymentMethodTypes = new List<string>
                    {
                        "card",
                    },
                    AutomaticTax = new SessionAutomaticTaxOptions { Enabled = true },
                    LineItems = new List<SessionLineItemOptions>(),
                    Currency = "usd",
                    Mode = "payment",
                    SuccessUrl = "http://localhost:4200/success.html",
                    CancelUrl = "http://localhost:4200/cancel.html",
                    CustomerEmail = customerFound.Email,
                    ClientReferenceId = cartSubmitted.Id.ToString()+1,
            };

            // Populate LineItems with detail from each ShoppingCart
            foreach (CartItemDTO item in cartSubmitted.Items)
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
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
            // return Ok(session);
           

            // return Ok("reached");
        }

        [HttpPost]
        [Route("cancel")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public string ORDER__CANCEL(int cartId)
        {
            
             * 
             * Process refund here
             * Notify customer here
             * 
            string result = $"cartId: {cartId}, return order currentstatus == cancelled";
            return result;
        }

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public string ORDER__UPDATE(OrderDTO DTO)
        {
             * 
             * Notify customer of order status change
             * 
            string result = $"order DTO, return updated order (ie: currentstatus == cooking/preparing/delivering/delivered/etc)";
            return result;
        }
        */
    }
}
