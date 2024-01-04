using AuthReadyAPI.DataLayer.Interfaces;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace AuthReadyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ICartRepository _cart;
        private readonly IOrderRepository _order;
        private readonly IAuthRepository _auth;

        public OrderController(ICartRepository cart, IOrderRepository order, IAuthRepository auth)
        {
            _cart = cart;
            _order = order;
            _auth = auth;
        }

        [HttpPost]
        [Route("prepare/{CartType}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PrepareOrder([FromRoute] string CartType, [FromBody] int CartId)
        {
            // The main goal of this is to grab the stripe url for a "successful" payment attempt.

            StringBuilder PaymentUrl = new StringBuilder();
            switch (CartType.ToLower())
            {
                case "single":
                    PaymentUrl.Append(await _order.PrepareCart(CartType.ToLower(), CartId));
                    break;

                case "auction":
                    PaymentUrl.Append(await _order.PrepareCart(CartType.ToLower(), CartId));
                    break;

                case "shopping":
                    PaymentUrl.Append(await _order.PrepareCart(CartType.ToLower(), CartId));
                    break;

                case "services":
                    PaymentUrl.Append(await _order.PrepareCart(CartType.ToLower(), CartId));
                    break;

                default:
                    return BadRequest("invalid url");
            }

            return Ok(PaymentUrl.ToString());
        }
    }
}
