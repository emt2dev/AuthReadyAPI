using AuthReadyAPI.DataLayer.DTOs.PII.Payments;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.DTOs.Services;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models.PII;
using AuthReadyAPI.DataLayer.Models.ServicesInfo;
using AuthReadyAPI.DataLayer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace AuthReadyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cart;
        private readonly IServicesRepository _services;
        private readonly IAuthRepository _authRepository;
        private readonly UserManager<APIUserClass> _userManager;
        APIUserClass _user;
        public CartController(ICartRepository cart, IServicesRepository services, UserManager<APIUserClass> userManager, IAuthRepository authRepository)
        {
            _cart = cart;
            _services = services;
            _authRepository = authRepository;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("get")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ShoppingCartDTO> GetUserCart(string UserId)
        {
            string Token = (string)HttpContext.Request.Headers["Authorization"];
            Token = Token.Replace("Bearer ", "");

            _user = await _userManager.FindByIdAsync(await _authRepository.ReadUserId(Token));
            if (_user is null) return null;

            return await _cart.GetUserCart(UserId);
        }

        [HttpPost]
        [Route("add")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<bool> AddItemToCart(AddProductToCartDTO IncomingDTO)
        {
            return await _cart.AddItem(IncomingDTO);
        }

        [HttpPost]
        [Route("remove")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<bool> RemoveItemFromCart(RemoveProductFromCartDTO IncomingDTO)
        {
            return await _cart.RemoveItem(IncomingDTO);
        }

        [HttpPost]
        [Route("new/product")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ShoppingCartDTO> GetNewCart(int CartId)
        {
            return await _cart.IssueNewCart(CartId);
        }

        // We return the cart in this endpoint for a better user experience.
        [HttpPost]
        [Route("appointment/schedule")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ServicesCartDTO> NewAppointment([FromForm] NewAppointmentDTO DTO)
        {
            List<AppointmentClass> List = await _services.ScheduleAppointment(DTO);

            return await _services.IssueNewServiceCart(List);
        }

        [HttpPost]
        [Route("new/single")]
        [Authorize(Roles = "Company")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<bool> AddNewSingleCart(NewSingleProductDTO DTO)
        {
            string Token = (string)HttpContext.Request.Headers["Authorization"];
            Token = Token.Replace("Bearer ", "");

            _user = await _userManager.FindByIdAsync(await _authRepository.ReadUserId(Token));
            if (_user is null) return false;

            return await _cart.AddSingleProductCart(DTO);
        }

        [HttpPost]
        [Route("new/auction")]
        [Authorize(Roles = "Company")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<bool> GetNewAuctionCart(NewAuctionProductDTO DTO)
        {
            string Token = (string)HttpContext.Request.Headers["Authorization"];
            Token = Token.Replace("Bearer ", "");

            _user = await _userManager.FindByIdAsync(await _authRepository.ReadUserId(Token));
            if (_user is null) return false;

            return await _cart.AddAuctionProductCart(DTO);
        }

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ShoppingCartDTO> UpdateCart(ShoppingCartDTO IncomingDTO)
        {
            return await _cart.UpdateCart(IncomingDTO);
        }

        [HttpGet]
        [Route("single/all")]
        [Authorize(Roles = "Company,User")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<SingleProductCartDTO>> GetSingleProductsCart()
        {
            string Token = (string)HttpContext.Request.Headers["Authorization"];
            Token = Token.Replace("Bearer ", "");

            _user = await _userManager.FindByIdAsync(await _authRepository.ReadUserId(Token));
            if (_user is null) return null;

            return await _cart.GetSingleProductCarts(_user.Id);
        }

        [HttpPost]
        [Route("single/new")]
        [Authorize(Roles = "Company")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<bool> CreateSingleProduct(NewSingleProductDTO DTO)
        {
            string Token = (string)HttpContext.Request.Headers["Authorization"];
            Token = Token.Replace("Bearer ", "");

            _user = await _userManager.FindByIdAsync(await _authRepository.ReadUserId(Token));
            if (_user is null) return false;

            return await _cart.AddSingleProductCart(DTO);
        }

        [HttpGet]
        [Route("auction/all")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<AuctionProductCartDTO>> GetAuctionCarts()
        {
            string Token = (string)HttpContext.Request.Headers["Authorization"];
            Token = Token.Replace("Bearer ", "");

            _user = await _userManager.FindByIdAsync(await _authRepository.ReadUserId(Token));
            if (_user is null) return null;

            return await _cart.GetAuctionProductCarts(_user.Id);
        }
    }
}
