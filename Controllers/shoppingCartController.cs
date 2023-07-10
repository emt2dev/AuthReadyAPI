using AuthReadyAPI.DataLayer.DTOs.Cart;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthReadyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class shoppingCartController : ControllerBase
    {
        private readonly ICompany _company;
        private readonly IUser _user;
        private readonly IProduct _product;
        private readonly IShoppingCart _cart;
        private readonly IOrder _order;

        private readonly ILogger<AuthController> _LOGS;
        private readonly IAuthManager _IAM;
        private readonly IMapper _mapper;

        private readonly UserManager<APIUser> _UM;

        public shoppingCartController(ICompany company, IUser user, IProduct product, IShoppingCart cart, IOrder order, ILogger<AuthController> LOGS, IAuthManager IAM, IMapper mapper, UserManager<APIUser> UM)
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

        [HttpPost]
        [Route("existing/{companyId}/{customerId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IActionResult> GetCart([FromRoute] int companyId, [FromRoute] string customerId)
        {
            var findShoppingCart = await _cart.GET__EXISTING__CART(companyId, customerId);

            // returns perfectly with empty items array (concerning)?
            if(findShoppingCart is null) {
                shoppingCart newCart = new shoppingCart {
                    customerId = customerId,
                    companyId = companyId.ToString(),
                    cost = 0.00,
                    submitted = false,
                    abandoned = false,
                    costInString = "",
                };

                newCart = await _cart.AddAsync(newCart);

                return Ok(newCart);
            } else if (findShoppingCart is not null) {
                findShoppingCart.costInString = findShoppingCart.cost.ToString("0.####");
                return Ok(findShoppingCart);
            }           

            return Ok("not null and is null");
        }

        [HttpPost]
        [Route("add/{productId}/{customerId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IActionResult> AddItemToCart([FromRoute] int productId, [FromRoute] string customerId)
        {
            var findProduct = await _product.GetAsyncById(productId);
            var companyId = int.Parse(findProduct.CompanyId);
            var findShoppingCart = await _cart.GET__EXISTING__CART(companyId, customerId);
            findShoppingCart.costInString = findShoppingCart.cost.ToString("0.####");
            double startPoint = findShoppingCart.cost;

            if(findProduct is not null && findShoppingCart is not null) {
                // determine if item already in the cart
                CartItem addItemToCart = new CartItem {
                    Id = findProduct.Id,
                    productId = findProduct.Id.ToString(),
                    name = findProduct.Name,
                    price = findProduct.Price_Current,
                    imageURL = findProduct.ImageURL,
                    count = 1, 
                    description = findProduct.Description,
                };

                if(findShoppingCart.Items is not null) {
                    foreach (CartItem item in findShoppingCart.Items)
                    {

                        if (item.productId == addItemToCart.productId) {
                            item.count++;
                            findShoppingCart.cost = findShoppingCart.cost + addItemToCart.price;
                            findShoppingCart.costInString = findShoppingCart.cost.ToString("0.####");

                            await _cart.UpdateAsync(findShoppingCart);

                            return Ok("Updated quantity");
                        }
                    }

                    double landmark = findShoppingCart.cost + addItemToCart.price;

                    if(startPoint < landmark) {
                        findShoppingCart.Items.Add(addItemToCart);
                        
                        findShoppingCart.cost = findShoppingCart.cost + addItemToCart.price;

                        findShoppingCart.costInString = findShoppingCart.cost.ToString("0.####");

                        await _cart.UpdateAsync(findShoppingCart);

                        return Ok("Item Added to Cart landmark");
                    }
                } else {
                        findShoppingCart.Items.Add(addItemToCart);
                        findShoppingCart.cost = findShoppingCart.cost + addItemToCart.price;

                        findShoppingCart.costInString = findShoppingCart.cost.ToString("0.####");

                        await _cart.UpdateAsync(findShoppingCart);
                        return Ok("Item Added to Cart else landmark");
                }
            } else if(findProduct is not null && findShoppingCart is null) {
                shoppingCart newCart = new shoppingCart {
                    customerId = customerId,
                    companyId = companyId.ToString(),
                    cost = 0.00,
                    submitted = false,
                    abandoned = false,
                    costInString = "",
                };

                CartItem addItemToCart = new CartItem {
                    productId = findProduct.Id.ToString(),
                    name = findProduct.Name,
                    price = findProduct.Price_Current,
                    imageURL = findProduct.ImageURL,
                    count = 1, 
                    description = findProduct.Description,
                };

                newCart.Items.Add(addItemToCart);
                newCart.cost = newCart.cost + addItemToCart.price;
                newCart.costInString = newCart.cost.ToString("0.####");

                newCart = await _cart.AddAsync(newCart);

                return Ok("Item Added to Cart fsc is null");
            } else {
                return Ok("Product not found");
            }

            return Ok("I dont know how this got reached");
        }

        [HttpPost]
        [Route("empty/{cartId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IActionResult> CART__EMPTY(int cartId)
        {

            shoppingCart cartSearchingFor = await _cart.GetAsyncById(cartId);

            if(cartSearchingFor is not null)
            {
                cartSearchingFor.abandoned = true;
                
                shoppingCart newCart = new shoppingCart {
                    customerId = cartSearchingFor.customerId,
                    companyId = cartSearchingFor.companyId,
                    cost = 0.00,
                    submitted = false,
                    abandoned = false,
                    costInString = cartSearchingFor.cost.ToString("0.##")
                };

                await _cart.UpdateAsync(cartSearchingFor);
                await _cart.AddAsync(newCart);
            }

            return Ok("Cart has been deleted");
        }

        [HttpPost]
        [Route("remove/{productId}/{customerId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IActionResult> removeItem(int productId, string customerId)
        {
            
            var findProduct = await _product.GetAsyncById(productId);
            var companyId = int.Parse(findProduct.CompanyId);
            var cartSearchingFor = await _cart.GET__EXISTING__CART(companyId, customerId);
            double startPoint = cartSearchingFor.cost;

            cartSearchingFor.cost = startPoint - findProduct.Price_Current;

            var itemFound = cartSearchingFor.Items
            .Where(found => found.productId == findProduct.Id.ToString());

            IList<CartItem> bag = itemFound.ToList<CartItem>();

            foreach (CartItem item in bag)
            {
                if(item.productId == productId.ToString())
                {
                    cartSearchingFor.cost = startPoint - item.price;

                    if(item.count == 0) cartSearchingFor.Items.Remove(item);
                    else item.count = item.count - 1;
                }
            }


            cartSearchingFor.costInString = cartSearchingFor.cost.ToString("0.####");

            await _cart.UpdateAsync(cartSearchingFor);
            return Ok("Cart has been updated");
        }
    }
}
