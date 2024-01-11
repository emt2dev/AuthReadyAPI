using AuthReadyAPI.DataLayer.DTOs.PII.APIUser;
using AuthReadyAPI.DataLayer.DTOs.PII.Payments;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models.PII;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthReadyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        private readonly IAuctionRepository _auction;
        private readonly IAuthRepository _authRepository;
        private readonly UserManager<APIUserClass> _userManager;
        APIUserClass _user;
        public AuctionController(IAuctionRepository auction, UserManager<APIUserClass> userManager, IAuthRepository authRepository)
        {
            _auction = auction;
            _userManager = userManager;
            _authRepository = authRepository;
        }

        // 
        /// <summary>
        /// Note this functionality needs real-time communication (ie websocket, SignalR, or something similiar) to be successful
        /// This shows the basic concept
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("active")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<AuctionProductDTO>> GetActiveAuctionProducts()
        {
            string Token = (string)HttpContext.Request.Headers["Authorization"];
            Token = Token.Replace("Bearer ", "");

            _user = await _userManager.FindByIdAsync(await _authRepository.ReadUserId(Token));
            if (_user is null) return null;

            return await _auction.GetActiveAuctionProducts();
        }

        [HttpGet]
        [Route("inactive")]
        [Authorize(Roles = "Company,Admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<AuctionProductDTO>> GetInactiveAuctionProducts()
        {
            string Token = (string)HttpContext.Request.Headers["Authorization"];
            Token = Token.Replace("Bearer ", "");

            _user = await _userManager.FindByIdAsync(await _authRepository.ReadUserId(Token));
            if (_user is null) return null;

            return await _auction.GetInactiveAuctionProducts();
        }

        [HttpPost]
        [Route("create/new")]
        [Authorize(Roles = "Company")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<bool> CreateAuctionProduct([FromBody]  NewAuctionProductDTO DTO)
        {
            string Token = (string)HttpContext.Request.Headers["Authorization"];
            Token = Token.Replace("Bearer ", "");

            _user = await _userManager.FindByIdAsync(await _authRepository.ReadUserId(Token));
            if (_user is null) return false;

            return await _auction.AddAuctionProduct(DTO);
        }

        [HttpPost]
        [Route("submit/bid")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<bool> NewBid(BidDTO DTO)
        {
            string Token = (string)HttpContext.Request.Headers["Authorization"];
            Token = Token.Replace("Bearer ", "");

            _user = await _userManager.FindByIdAsync(await _authRepository.ReadUserId(Token));
            if (_user is null) return false;

            return await _auction.AddBid(DTO);
        }

    }
}
