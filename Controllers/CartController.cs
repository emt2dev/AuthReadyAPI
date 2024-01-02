using AuthReadyAPI.DataLayer.DTOs.PII.Payments;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthReadyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICart _cart;
        public CartController(ICart cart)
        {
            _cart = cart;
        }

        [HttpGet]
        [Route("get")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ShoppingCartDTO> GetUserCart(string UserId)
        {
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
        [Route("new")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ShoppingCartDTO> GetNewCart(int CartId)
        {
            return await _cart.IssueNewCart(CartId);
        }

        [HttpPost]
        [Route("update")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ShoppingCartDTO> UpdateItemQuantity(ShoppingCartDTO IncomingDTO)
        {
            return await _cart.UpdateCart(IncomingDTO);
        }

        [HttpGet]
        [Route("single/all")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<SingleProductCartDTO>> GetSingleProductsCart()
        {
            // User ID from Jwt
            return await _cart.GetSingleProductCarts("1");
        }

        [HttpPost]
        [Route("single/new")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<bool> CreateSingleProduct(NewSingleProductDTO DTO)
        {
            return await _cart.AddSingleProductCart(DTO);
        }

        [HttpGet]
        [Route("auction/all")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<AuctionProductCartDTO>> GetAuctionCarts()
        {
            // User ID from Jwt
            return await _cart.GetAuctionProductCarts("1");
        }
    }
}
