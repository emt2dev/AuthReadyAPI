using System.Text.Json;
using AuthReadyAPI.DataLayer.DTOs.APIUser;
using AuthReadyAPI.DataLayer.DTOs.AuthResponse;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthReadyAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/mobile/delivery")]
    [ApiVersion("2.0")]
    [Consumes("application/x-www-form-urlencoded")]
    public class v2_MobileDeliveryController : ControllerBase
    {
        private readonly ILogger<v2_MobileDeliveryController> _LOGS;
        private readonly IMapper _mapper;
        private readonly IV2_AuthManager _IAM;
        private readonly v2_UserStripe? _customer;
        private readonly IV2_Order _order;
        private readonly String readyForDelivery = "ready for delivery";
        private readonly String acceptedDelivery = "out for delivery";
        private readonly String deliveryFinished = "order was delivered";
        private readonly UserManager<v2_UserStripe> _UM;
        private readonly int company = 1;
        public v2_MobileDeliveryController(ILogger<v2_MobileDeliveryController> LOGS, IV2_Order order, IMapper mapper, IV2_AuthManager IAM, UserManager<v2_UserStripe> UM)
        {
            this._LOGS = LOGS;
            this._mapper = mapper;
            this._IAM = IAM;
            this._order = order;
            this._UM = UM;
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<ActionResult> mobileLoginStaff([FromForm] Base__APIUser DTO)
        {
            Full__AuthResponseDTO authenticatedUser = await _IAM.loginStaff(DTO);

            if (authenticatedUser == null)
            {
                _LOGS.LogInformation($"Failed Login Attempt for {DTO.Email}");
                return Unauthorized();
            }

            return Ok(authenticatedUser);
        }

        [HttpGet]
        [Route("orders/{companyId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<ActionResult> getOrdersReadyForDelivery([FromRoute] int companyId)
        {
            IList<v2_Order> orderList = await _order.getReadyDeliveryOrders(companyId);
            IList<v2_Order> orderList2 = await _order.getAllCompanyOrders(companyId);

            foreach (v2_Order order in orderList)
            {
                orderList2.Add(order);
            }
            
            return new JsonResult(orderList2, new JsonSerializerOptions { PropertyNamingPolicy = null});
        }

        [HttpGet]
        [Route("completed/{orderId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<ActionResult> deliveryCompleted([FromRoute] int orderId)
        {
            v2_Order orderCompleted = await _order.GetAsyncById(orderId);
            orderCompleted.status = this.deliveryFinished;
            orderCompleted.timeDelivered = DateTime.Now;
            orderCompleted.orderCompleted = true;
            orderCompleted.eta = "";

            await _order.UpdateAsync(orderCompleted);

            var i = new JsonResult("order completed", new JsonSerializerOptions { PropertyNamingPolicy = null});
            return i;
        }

        [HttpGet]
        [Route("accept/{orderId}/{staffId}/{eta}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<ActionResult> deliveryAccepted([FromRoute] int orderId, [FromRoute] int staffId, [FromRoute] string eta)
        {
            v2_Order acceptedOrder = await _order.GetAsyncById(orderId);
            acceptedOrder.status = this.acceptedDelivery;
            acceptedOrder.eta = eta;

            await _order.UpdateAsync(acceptedOrder);

            var i = new JsonResult("order accepted", new JsonSerializerOptions { PropertyNamingPolicy = null});
            return i;
        }
    }
}