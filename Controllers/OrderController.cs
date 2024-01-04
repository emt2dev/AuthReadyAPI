using AuthReadyAPI.DataLayer.Interfaces;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;

namespace AuthReadyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ICartRepository _cart;
        private readonly IProductOrderRepository _order;

        public OrderController(ICartRepository cart, IProductOrderRepository order)
        {
            _cart = cart;
            _order = order;
        }
    }
}
