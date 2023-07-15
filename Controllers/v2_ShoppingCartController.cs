using System.Text.Json;
using AuthReadyAPI.DataLayer.DTOs.Cart;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthReadyAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/shoppingCart")]
    [ApiVersion("2.0")]
    [Authorize(Roles = "Customer,Staff,Owner")]
    public class v2_ShoppingCartController : ControllerBase
    {
        private readonly IV2_ShoppingCart _cart;
        private readonly IMapper _mapper;
        private readonly ILogger<v2_ShoppingCartController> _LOGS;
        private readonly IV2_Product _product;
        public v2_ShoppingCartController(IV2_Product product, ILogger<v2_ShoppingCartController> LOGS, IV2_ShoppingCart cart, IMapper mapper)
        {
            this._LOGS = LOGS;
            this._mapper = mapper;
            this._cart = cart;
            this._product = product;
        }

        [HttpPost]
        [Route("existing/{companyId}/{customerId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IActionResult> GetCart([FromRoute] int companyId, [FromRoute] string customerId)
        {
            v2_ShoppingCart findShoppingCart = await _cart.getExistingShoppingCart(companyId, customerId);

            // returns perfectly with empty items array (concerning)?
            if(findShoppingCart is null) {
                v2_ShoppingCart newCart = new v2_ShoppingCart {
                    customerId = customerId,
                    companyId = companyId.ToString(),
                    cost = 0.00,
                    submitted = false,
                    abandoned = false,
                    costInString = "",
                };

                newCart = await _cart.AddAsync(newCart);

                v2_ShoppingCartDTO outgoingDTO = _mapper.Map<v2_ShoppingCartDTO>(newCart);
                return Ok(newCart);
            } else if (findShoppingCart is not null) {
                findShoppingCart.costInString = findShoppingCart.cost.ToString("0.####");

                v2_ShoppingCartDTO outgoingDTO = _mapper.Map<v2_ShoppingCartDTO>(findShoppingCart);

                IList<v2_ProductDTO> outgoingProductDTOsList = new List<v2_ProductDTO>();

                foreach (v2_ProductStripe product in findShoppingCart.Items)
                {
                    v2_ProductDTO outgoingProductDTO =_mapper.Map<v2_ProductDTO>(product);
                    outgoingProductDTOsList.Add(outgoingProductDTO);
                }
                
                outgoingDTO.Items = outgoingProductDTOsList;
                return Ok(outgoingDTO);
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
            v2_ProductStripe findProduct = await _product.GetAsyncById(productId);
            findProduct.quantity = 1;
            
            int companyId = findProduct.companyId;

            v2_ShoppingCart findShoppingCart = await _cart.getExistingShoppingCart(companyId, customerId);
            findShoppingCart.costInString = findShoppingCart.cost.ToString("0.####");
            double startPoint = findShoppingCart.cost;

            if(findProduct is not null && findShoppingCart is not null) {

                v2_ProductDTO addItemToCart = _mapper.Map<v2_ProductDTO>(findProduct);
                addItemToCart.quantity = 1;

                if(findShoppingCart.Items is not null) {
                    foreach (v2_ProductStripe item in findShoppingCart.Items)
                    {

                        if (item.id == addItemToCart.id) {
                            item.quantity++;
                            findShoppingCart.cost = findShoppingCart.cost + addItemToCart.default_price;
                            findShoppingCart.costInString = findShoppingCart.cost.ToString("0.####");

                            await _cart.UpdateAsync(findShoppingCart);

                            return Ok("Updated quantity");
                        }
                    }

                    double landmark = findShoppingCart.cost + addItemToCart.default_price;

                    if(startPoint < landmark) {
                        findShoppingCart.Items.Add(findProduct);
                        
                        findShoppingCart.cost = findShoppingCart.cost + findProduct.default_price;

                        findShoppingCart.costInString = findShoppingCart.cost.ToString("0.####");

                        await _cart.UpdateAsync(findShoppingCart);

                        return Ok("Item Added to Cart landmark");
                    }
                } else {
                        findShoppingCart.Items.Add(findProduct);
                        findShoppingCart.cost = findShoppingCart.cost + addItemToCart.default_price;

                        findShoppingCart.costInString = findShoppingCart.cost.ToString("0.####");

                        await _cart.UpdateAsync(findShoppingCart);
                        return Ok("Item Added to Cart else landmark");
                }
            } else if(findProduct is not null && findShoppingCart is null) {
                v2_ShoppingCart newCart = new v2_ShoppingCart {
                    customerId = customerId,
                    companyId = companyId.ToString(),
                    cost = 0.00,
                    submitted = false,
                    abandoned = false,
                    costInString = "",
                };

                newCart.Items.Add(findProduct);
                newCart.cost = newCart.cost + findProduct.default_price;
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

            v2_ShoppingCart cartSearchingFor = await _cart.GetAsyncById(cartId);

            if(cartSearchingFor is not null)
            {
                cartSearchingFor.abandoned = true;
                
                v2_ShoppingCart newCart = new v2_ShoppingCart {
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
        public async Task<IActionResult> removeItem([FromRoute] int productId, [FromRoute] string customerId)
        {
            
            v2_ProductStripe findProduct = await _product.GetAsyncById(productId);
            v2_ShoppingCart cartSearchingFor = await _cart.getExistingShoppingCart(findProduct.companyId, customerId);
            double startPoint = cartSearchingFor.cost;

            cartSearchingFor.cost = startPoint - findProduct.default_price;

            var itemFound = cartSearchingFor.Items
            .Where(found => found.id == findProduct.id);

            IList<v2_ProductStripe> bag = itemFound.ToList<v2_ProductStripe>();

            foreach (v2_ProductStripe item in bag)
            {
                if(item.id == findProduct.id)
                {
                    cartSearchingFor.cost = startPoint - item.default_price;

                    if(item.quantity == 0) cartSearchingFor.Items.Remove(item);
                    else item.quantity = item.quantity - 1;
                }
            }

            cartSearchingFor.costInString = cartSearchingFor.cost.ToString("0.####");

            await _cart.UpdateAsync(cartSearchingFor);
            return Ok("Cart has been updated");
        }
    }
}
