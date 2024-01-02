using AuthReadyAPI.DataLayer.Interfaces;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;

namespace AuthReadyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ICart _cart;
        private readonly IProductOrder _order;

        public OrderController(ICart cart, IProductOrder order)
        {
            _cart = cart;
            _order = order;
        }
    }
}
