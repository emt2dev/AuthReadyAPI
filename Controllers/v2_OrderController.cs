using AuthReadyAPI.DataLayer.DTOs.APIUser;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AuthReadyAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/orders")]
    [ApiVersion("2.0")]
    public class v2_OrderController : ControllerBase
    {
        private readonly ILogger<v2_OrderController> _LOGS;
        private readonly IMapper _mapper;
        private readonly IV2_AuthManager _IAM;
        private readonly v2_UserStripe? _user;
        private readonly IStripeService _ss;
        private readonly IV2_ShoppingCart _cart;
        private readonly IV2_Order _order;
        private readonly String orderReceived = "order received";
        private readonly String defaultETA = "To be determined";
        private readonly String readyForPickup = "ready for pickup";
        private readonly String readyForDelivery = "ready for delivery";
        private readonly String acceptedDelivery = "out for delivery";
        private readonly String deliveryFinished = "order was delivered";
        private readonly String takeoutFinished = "order was picked up";
        private readonly String methodDelivery = "Delivery";
        private readonly String methodPickup = "Pick up";
        private readonly string staffDashboard = "http://localhost:4200/staff";
        public v2_OrderController(ILogger<v2_OrderController> LOGS, IMapper mapper, IV2_AuthManager IAM, IV2_Order order, IStripeService ss, IV2_ShoppingCart cart)
        {
            this._LOGS = LOGS;
            this._mapper = mapper;
            this._IAM = IAM;
            this._cart = cart;
            this._ss = ss;
            this._order = order;
        }

        [HttpGet]
        [Route("delivery/ready/{orderId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<ActionResult> deliveryReady([FromRoute] int orderId)
        {
            v2_Order acceptedOrder = await _order.GetAsyncById(orderId);
            acceptedOrder.status = this.readyForDelivery;
            acceptedOrder.eta = DateTime.Now.AddMinutes(33).ToString();

            await _order.UpdateAsync(acceptedOrder);            

            System.Uri uri = new System.Uri(staffDashboard);

            return Redirect(staffDashboard);
        }

        [HttpGet]
        [Route("delivery/completed/{orderId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<ActionResult> deliveryComplete([FromRoute] int orderId)
        {
            v2_Order acceptedOrder = await _order.GetAsyncById(orderId);
            acceptedOrder.status = this.deliveryFinished;
            acceptedOrder.eta = deliveryFinished;
            acceptedOrder.orderCompleted = true;

            await _order.UpdateAsync(acceptedOrder);

            System.Uri uri = new System.Uri(staffDashboard);

            return Redirect(staffDashboard);
        }

        [HttpGet]
        [Route("takeout/ready/{orderId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<ActionResult> takeoutReady([FromRoute] int orderId)
        {
            v2_Order acceptedOrder = await _order.GetAsyncById(orderId);
            acceptedOrder.status = readyForPickup;
            acceptedOrder.eta = readyForPickup;

            await _order.UpdateAsync(acceptedOrder);
            
            System.Uri uri = new System.Uri(staffDashboard);

            return Redirect(staffDashboard);
        }

        [HttpGet]
        [Route("takeout/completed/{orderId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<ActionResult> takeoutComplete([FromRoute] int orderId)
        {
            v2_Order acceptedOrder = await _order.GetAsyncById(orderId);
            acceptedOrder.status = this.takeoutFinished;
            acceptedOrder.eta = takeoutFinished;
            acceptedOrder.orderCompleted = true;

            await _order.UpdateAsync(acceptedOrder);

            return Ok(takeoutFinished);
        }

        [HttpPost]
        [Route("submit/delivery/{companyId}/{customerId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<JsonResult> newDeliveryOrder([FromRoute] int companyId, string customerId)
        {
            v2_ShoppingCart cartSubmitted = await _cart.getExistingShoppingCart(companyId, customerId);
            v2_UserStripe customerFound = await _IAM.getCustomerDetails(customerId);
            v2_CustomerDTO outgoingDTO = _mapper.Map<v2_CustomerDTO>(customerFound);
            var sessionId = await _ss.v2_CheckOut(cartSubmitted, outgoingDTO);

            string addressBuilder = $"{customerFound.addressStreet} {customerFound.addressSuite}, {customerFound.addressCity}, {customerFound.addressState} {customerFound.addressPostal_code} {customerFound.addressCountry}";

            v2_Order newOrder = new v2_Order {
                cart = cartSubmitted,
                delivery = true,
                pickedUpByCustomer = false,
                orderCompleted = false,
                deliveryAddress = addressBuilder,
                status = orderReceived,
                eta = defaultETA,
                method = methodDelivery,
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
            v2_UserStripe customerFound = await _IAM.getCustomerDetails(customerId);
            v2_CustomerDTO outgoingDTO = _mapper.Map<v2_CustomerDTO>(customerFound);
            var sessionId = await _ss.v2_CheckOut(cartSubmitted, outgoingDTO);

            v2_Order newOrder = new v2_Order {
                cart = cartSubmitted,
                delivery = false,
                pickedUpByCustomer = false,
                orderCompleted = false,
                status = orderReceived,
                eta = defaultETA,
                method = methodPickup,
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
        public async Task<IList<v2_OrderDTO>> getAllCustomerOrders([FromRoute] int companyId, string customerId)
        {
            IList<v2_Order> allOrdersList = await _order.getAllCustomerOrders(companyId, customerId);
            IList<v2_OrderDTO> listOfDTOs = _mapper.Map<IList<v2_OrderDTO>>(allOrdersList);

            return listOfDTOs;
        }

        [HttpGet]
        [Route("active/{companyId}/{customerId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay

        public async Task<IList<v2_OrderDTO>> getActiveCustomerOrders([FromRoute] int companyId, string customerId)
        {
            IList<v2_Order> allOrdersList = await _order.getActiveCustomerOrders(companyId, customerId);
            IList<v2_OrderDTO> listOfDTOs = _mapper.Map<IList<v2_OrderDTO>>(allOrdersList);
            return listOfDTOs;
        }

        [HttpGet]
        [Route("completed/{companyId}/{customerId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay

        public async Task<IList<v2_OrderDTO>> getCompletedCustomerOrders([FromRoute] int companyId, string customerId)
        {
            IList<v2_Order> allOrdersList = await _order.getCompletedCustomerOrders(companyId, customerId);
            IList<v2_OrderDTO> listOfDTOs = _mapper.Map<IList<v2_OrderDTO>>(allOrdersList);
            return listOfDTOs;
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