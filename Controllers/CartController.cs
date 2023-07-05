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
    public class CartController : ControllerBase
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

        public CartController(ICompany company, IUser user, IProduct product, ICart cart, IOrder order, ILogger<AuthController> LOGS, IAuthManager IAM, IMapper mapper, UserManager<APIUser> UM)
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

        // api/cart/existing/{companyId}/{customerId}
        [HttpPost]
        [Route("existing/{companyId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<Cart> CART__GET__EXISTING(int companyId, string userEmail)
        {
            APIUser usersGiven = await _UM.FindByEmailAsync(userEmail);

            Cart cartSearchingFor = await _cart.GET__EXISTING__CART(companyId, usersGiven.Id);
            IList<Full__Product> ProductDTOs = new List<Full__Product>();

            return cartSearchingFor;

            // foreach (var product in cartSearchingFor.Products)
            // {
            //     Full__Product mappedProduct = _mapper.Map<Full__Product>(product);
            //     ProductDTOs.Add(mappedProduct);
            // }

            // if (cartSearchingFor is not null)
            // {
            //     Full__Cart mappedCart = new Full__Cart {
            //         Id = cartSearchingFor.Id.ToString(),
            //         Customer_Id = cartSearchingFor.Customer,
            //         CompanyId = cartSearchingFor.Company,
            //         Products = ProductDTOs,
            //         Total_Amount = cartSearchingFor.Total_Amount,
            //         Abandoned = cartSearchingFor.Abandoned,
            //         Submitted = cartSearchingFor.Submitted,
            //         Total_Discounted = cartSearchingFor.Total_Discounted,
            //         Discount_Rate = cartSearchingFor.Discount_Rate,
            //     };
            //     return mappedCart;

            // } else {
            //     Cart newCart = new Cart {
            //         Customer = usersGiven.Id,
            //         Company = companyId.ToString(),
            //         Total_Amount = 0.00,
            //         Submitted = false,
            //     };

            //     newCart = await _cart.AddAsync(newCart);

            //     Full__Cart mappedCart = new Full__Cart {
            //         Id = newCart.Id.ToString(),
            //         Customer_Id = newCart.Customer,
            //         CompanyId = newCart.Company,
            //         Products = null,
            //         Total_Amount = newCart.Total_Amount,
            //         Abandoned = newCart.Abandoned,
            //         Submitted = newCart.Submitted,
            //         Total_Discounted = newCart.Total_Discounted,
            //         Discount_Rate = newCart.Discount_Rate,
            //     };

            //     return mappedCart;
            // }
        }
        /*
        [HttpGet]
        [Route("existing/{companyId}/{customerId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public string CART__GET__EXISTING(int companyId, int customerId)
        {
            string result = $"company: {companyId}, customer: {customerId}";
            return result;
        }
*/
        /*
        [HttpPost]
        [Route("new/{companyId}/{customerId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public string CART__CREATE__NEW(int companyId, int customerId)
        {
            string result = $"company: {companyId}, customer: {customerId}";
            return result;
        }
*/

        // api/cart/add/{companyId}/{customerId}
        [HttpPost]
        [Route("add/{companyId}/{productId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<Cart> CART__ADD__PRODUCT(int companyId, int productId, [FromForm] string userEmail)
        {
            
            //  * 
            //  *  flowchart
            //  *  if no cart exists with customer and company ids and submitted != true, create cart, add product, return cart
            //  *  if cart == abandoned, change that to false and use that cart
            //  *  if cart exists add product to cart, return cart
            //  *
            APIUser usersGiven = await _UM.FindByEmailAsync(userEmail);
            var productToAdd = await _product.GetAsyncById(productId);

            var cartSearchingFor = await _cart.GET__EXISTING__CART(companyId, usersGiven.Id);
            Product newQuantity = cartSearchingFor.Products.FirstOrDefault<Product>(productToAdd);
            newQuantity.Quanity += 1;

            
            cartSearchingFor.Products.Remove(productToAdd);
            cartSearchingFor.Products.Add(newQuantity);
            
            await _cart.UpdateAsync(cartSearchingFor);

            return cartSearchingFor;
        }

        [HttpPost]
        [Route("empty/{cartId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IActionResult> CART__EMPTY(int cartId)
        {
            
            //  * 
            //  *  flowchart
            //  *  if no cart exists with customer and company ids and submitted != true, create cart, add product, return cart
            //  *  if cart == abandoned, change that to false and use that cart
            //  *  if cart exists add product to cart, return cart
            //  *
            Cart cartSearchingFor = await _cart.GetAsyncById(cartId);
            // IList<Product> newList = new List<Product>();
            
            // Cart newCart = new Cart {
            //     Customer = cartSearchingFor.Customer,
            //     Company = cartSearchingFor.Company,
            //     Total_Amount = 0.00,
            //     Total_Discounted = 0.00,
            //     Abandoned = false,
            //     Submitted = false,
            //     Products = newList,
            // };

            await _cart.DeleteAsync(cartId);

            // await _cart.AddAsync(newCart);

            return Ok("Cart has been deleted");
        }
        /*
        [HttpGet]
        [Route("add/{companyId}/{customerId}/{productId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public string CART__ADD__PRODUCT(int companyId, int customerId, int productId)
        {
            /*
             * 
             *  flowchart
             *  if no cart exists with customer and company ids and submitted != true, create cart, add product, return cart
             *  if cart == abandoned, change that to false and use that cart
             *  if cart exists add product to cart, return cart
             *
            
            string result = $"company: {companyId}, customer: {customerId}, product: {productId}";
            return result;
        }
        */
    }
}
