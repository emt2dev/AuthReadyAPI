using AuthReadyAPI.DataLayer.DTOs.Order;
using AuthReadyAPI.DataLayer.DTOs.Pagination;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ICart _cart;
        private readonly IOrder _order;

        private readonly ILogger<AuthController> _LOGS;
        private readonly IAuthManager _IAM;
        private readonly IMapper _mapper;

        private readonly UserManager<APIUser> _UM;

        public OrderController(ICompany company, IUser user, IProduct product, ICart cart, IOrder order, ILogger<AuthController> LOGS, IAuthManager IAM, IMapper mapper, UserManager<APIUser> UM)
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

        /* api/order/submit */
        [HttpPost]
        [Route("submit")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public string ORDER__SUBMITTED([FromBody] int shoppingCartId)
        {
            /*
             * 
             * Process stripe api payment here
             * Notify Company of new order
             * Notify customer of payment processed
             */
            string result = $"shoppingCartId: {shoppingCartId}, return order";
            return result;
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
