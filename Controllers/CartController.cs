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
        [Route("existing/{companyId}/{userId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<Cart> CART__GET__EXISTING([FromRoute] int companyId, [FromRoute] string userId)
        {
            APIUser usersGiven = await _UM.FindByIdAsync(userId);
            Cart cartSearchingFor = await _cart.GET__EXISTING__CART(companyId, usersGiven.Id);
            return cartSearchingFor;
            // Full__Product productToAdd = new Full__Product {
            //     Name = "PlaceHolder",
            //     Description = "PlaceHolder",
            //     Price_Current = 0.00,
            //     CompanyId = companyId.ToString(),
            //     Price_Normal = 0.00,
            //     Price_Sale = 0.00,
            //     ImageURL = "https://placehold.it/150x80?text=IMAGE",
            //     Modifiers = "none",
            //     Keyword = "none",
            //     Quantity = 1,
            // };

            // if(cartSearchingFor is not null && cartSearchingFor.Products is not null) {
            //     return cartSearchingFor;
            // } else if(cartSearchingFor.Products is null) {

            //     cartSearchingFor.Products.Add(productToAdd);
            //     cartSearchingFor.Total_Amount =  cartSearchingFor.Total_Amount + productToAdd.Price_Current;

            //     await _cart.UpdateAsync(cartSearchingFor);

            //     return cartSearchingFor;
            // } else {
            //     Cart newCart = new Cart {
            //         Customer = usersGiven.Id,
            //         Company = companyId.ToString(),
            //         Total_Amount = 0.00,
            //         Submitted = false,
            //     };

            //     newCart.Products.Add(productToAdd);

            //     return newCart;
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
        [Route("add/{companyId}/{productId}/{userId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<Full__Cart> CART__ADD__PRODUCT([FromRoute] int companyId, [FromRoute] int productId, [FromRoute] string userId)
        {
            //  * 
            //  *  flowchart
            //  *  if no cart exists with customer and company ids and submitted != true, create cart, add product, return cart
            //  *  if cart == abandoned, change that to false and use that cart
            //  *  if cart exists add product to cart, return cart
            //  *
            APIUser usersGiven = await _UM.FindByIdAsync(userId);
            Product ProductToAdd = await _product.GetAsyncById(productId);
            Full__Product mappedProduct = _mapper.Map<Full__Product>(ProductToAdd);
            mappedProduct.Quantity = 1;

            Cart cartSearchingFor = await _cart.GET__EXISTING__CART(companyId, usersGiven.Id);
            // return cartSearchingFor;

            if(cartSearchingFor is not null && cartSearchingFor.Products is not null) {
                var j = cartSearchingFor.Products.IndexOf(mappedProduct);
                if (j > -1)
                {
                    var i = cartSearchingFor.Products.IndexOf(mappedProduct);
                    cartSearchingFor.Products[i].Quantity = cartSearchingFor.Products[i].Quantity++;
                    cartSearchingFor.Total_Amount = cartSearchingFor.Total_Amount + cartSearchingFor.Products[i].Price_Current;
                } else {
                    cartSearchingFor.Products.Add(mappedProduct);
                    cartSearchingFor.Total_Amount = cartSearchingFor.Total_Amount + mappedProduct.Price_Current;
                }

                await _cart.UpdateAsync(cartSearchingFor);

                Full__Cart CartDTO = new Full__Cart {
                    Id = cartSearchingFor.Id.ToString(),
                    Customer_Id = cartSearchingFor.Customer,
                    CompanyId = cartSearchingFor.Company,
                    Total_Amount = cartSearchingFor.Total_Amount,
                    Total_Discounted = cartSearchingFor.Total_Discounted,
                    Discount_Rate = cartSearchingFor.Discount_Rate,
                    Products = cartSearchingFor.Products,
                    Submitted = cartSearchingFor.Submitted,
                    Abandoned = cartSearchingFor.Abandoned,
                };

                return CartDTO;
            } else if(cartSearchingFor is not null && cartSearchingFor.Products is null) {
                cartSearchingFor.Products.Add(mappedProduct);
                cartSearchingFor.Total_Amount = cartSearchingFor.Total_Amount + mappedProduct.Price_Current;

                _cart.UpdateAsync(cartSearchingFor);

                Full__Cart CartDTO = new Full__Cart {
                    Id = cartSearchingFor.Id.ToString(),
                    Customer_Id = cartSearchingFor.Customer,
                    CompanyId = cartSearchingFor.Company,
                    Total_Amount = cartSearchingFor.Total_Amount,
                    Total_Discounted = cartSearchingFor.Total_Discounted,
                    Discount_Rate = cartSearchingFor.Discount_Rate,
                    Products = cartSearchingFor.Products,
                    Submitted = cartSearchingFor.Submitted,
                    Abandoned = cartSearchingFor.Abandoned,
                };

                return CartDTO;
            }
            
            
            else {
                Full__Cart CartDTO = new Full__Cart {
                    Id = "0",
                    Customer_Id = "0",
                    CompanyId = "0",
                    Total_Amount = 0,
                    Total_Discounted = 0,
                    Discount_Rate = 0,
                    Submitted = false,
                    Abandoned = false,
                };

                CartDTO.Products.Add(mappedProduct);

                return CartDTO;
            }
            // } else {
            //     productList.Add(mappedProduct);

            //     Cart newCart = new Cart {
            //         Customer = usersGiven.Id,
            //         Company = companyId.ToString(),
            //         Total_Amount = mappedProduct.Price_Current,
            //         Total_Discounted = 0.00,
            //         Discount_Rate = 0,
            //         Abandoned = false,
            //         Submitted = false,
            //         Products = productList,
            //     };

            //     if(newCart.Products.Contains(mappedProduct) is false) newCart.Products.Add(mappedProduct);

            //     newCart = await _cart.AddAsync(newCart);

            //     Full__Cart CartDTO = new Full__Cart {
            //         Id = newCart.Id.ToString(),
            //         Customer_Id = newCart.Customer,
            //         CompanyId = newCart.Company,
            //         Total_Amount = newCart.Total_Amount,
            //         Total_Discounted = newCart.Total_Discounted,
            //         Discount_Rate = newCart.Discount_Rate,
            //         Products = newCart.Products,
            //         Submitted = newCart.Submitted,
            //         Abandoned = newCart.Abandoned,
            //     };

            //     return CartDTO;
            // }
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

            if(cartSearchingFor is not null)
            {
                cartSearchingFor.Abandoned = true;
            
                IList<Product> newList = new List<Product>();
                
                Cart newCart = new Cart {
                    Customer = cartSearchingFor.Customer,
                    Company = cartSearchingFor.Company,
                    Total_Amount = 0.00,
                    Total_Discounted = 0.00,
                    Abandoned = false,
                    Submitted = false,
                    // Products = newList,
                };

                await _cart.UpdateAsync(cartSearchingFor);
                await _cart.AddAsync(newCart);
            }

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
