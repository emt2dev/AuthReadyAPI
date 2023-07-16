using System.Text.Json;
using AuthReadyAPI.DataLayer.DTOs.APIUser;
using AuthReadyAPI.DataLayer.DTOs.AuthResponse;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.BillingPortal;

namespace AuthReadyAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/mobile/customer")]
    [ApiVersion("2.0")]
    
    public class v2_MobileCustomerController : ControllerBase
    {
        private readonly ILogger<v2_MobileCustomerController> _LOGS;
        private readonly IMapper _mapper;
        private readonly IV2_AuthManager _IAM;
        private readonly v2_UserStripe? _customer;
        private readonly UserManager<v2_UserStripe> _UM;
        private readonly IV2_ShoppingCart _cart;
        private readonly IStripeService _ss;
        private readonly IV2_Order _order;
        private readonly IV2_Product _product;
        private readonly IConfiguration _configs;
        private readonly StripeClient _stripeClient;
        private readonly String paymentSuccess =  "Payment was a success";
        private readonly String orderReceived = "order received";
        private readonly String defaultETA = "To be determined";
        private readonly String readyForPickup = "ready for pickup";
        private readonly String readyForDelivery = "ready for delivery";
        private readonly String acceptedDelivery = "out for delivery";
        private readonly String deliveryFinished = "order was delivered";
        private readonly String takeoutFinished = "order was picked up";
        private readonly String methodDelivery = "Delivery";
        private readonly String methodPickup = "Pick up";
        public v2_MobileCustomerController(IConfiguration configs, ILogger<v2_MobileCustomerController> LOGS, IV2_ShoppingCart cart, IV2_Product product,  UserManager<v2_UserStripe> UM, IV2_Order order, IStripeService ss, IMapper mapper, IV2_AuthManager IAM)
        {
            this._LOGS = LOGS;
            this._mapper = mapper;
            this._IAM = IAM;
            this._UM = UM;
            this._ss = ss;
            this._order = order;
            this._cart = cart;
            this._product = product;
            this._configs = configs;
            this._stripeClient = new StripeClient(_configs["Stripe:SecretKey"]);
        }

        [HttpGet]
        [Route("intent/{cartId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IActionResult> mobileIntent([FromRoute] int cartId)
        {
            v2_ShoppingCart cartSearchingFor = await _cart.GetAsyncById(cartId);
            
            var l = cartSearchingFor.cost;

            v2_UserStripe userFound = await _UM.FindByIdAsync(cartSearchingFor.customerId);
            
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(cartSearchingFor.cost * 100),
                Currency = userFound.currency
            };

            var service = new PaymentIntentService(_stripeClient);
            var intent = await service.CreateAsync(options);

            return Ok(new
                {
                    ClientSecret = intent.ClientSecret,
                });

        }
        
        [HttpPost]
        [Route("empty/{cartId}")]
        [Consumes("application/x-www-form-urlencoded")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IActionResult> mobileEmptyCart(int cartId)
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

        [HttpGet]
        [Route("existing/{companyId}/{customerId}")]
        [Consumes("application/x-www-form-urlencoded")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<JsonResult> mobileGetCart([FromRoute] int companyId, [FromRoute] string customerId)
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
                var i = new JsonResult(outgoingDTO, new JsonSerializerOptions { PropertyNamingPolicy = null});
                return i;

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
                var i = new JsonResult(outgoingDTO, new JsonSerializerOptions { PropertyNamingPolicy = null});
                return i;
            }           

            var j = new JsonResult("no cart", new JsonSerializerOptions { PropertyNamingPolicy = null});
                return j;
        }

        [HttpGet]
        [Route("remove/{productId}/{customerId}")]
        [Consumes("application/x-www-form-urlencoded")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IActionResult> mobileRemoveItem([FromRoute] int productId, [FromRoute] string customerId)
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

        [HttpGet]
        [Route("add/{productId}/{customerId}")]
        [Consumes("application/x-www-form-urlencoded")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IActionResult> mobileAddItemToCart([FromRoute] int productId, [FromRoute] string customerId)
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
        [Route("submit/delivery/{companyId}/{customerId}")]
        [Consumes("application/x-www-form-urlencoded")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<JsonResult> mobileNewDeliveryOrder([FromRoute] int companyId, string customerId)
        {
            v2_ShoppingCart cartSubmitted = await _cart.getExistingShoppingCart(companyId, customerId);
            v2_UserStripe customerFound = await _IAM.getCustomerDetails(customerId);
            v2_CustomerDTO outgoingDTO = _mapper.Map<v2_CustomerDTO>(customerFound);

            string addressBuilder = $"{customerFound.addressStreet} {customerFound.addressSuite}, {customerFound.addressCity}, {customerFound.addressState} {customerFound.addressPostal_code} {customerFound.addressCountry}";

            v2_Order newOrder = new v2_Order {
                cart = cartSubmitted,
                delivery = true,
                pickedUpByCustomer = false,
                orderCompleted = false,
                deliveryAddress = addressBuilder,
                status = orderReceived,
                eta = defaultETA,
                method = methodDelivery,
            };

            _ = await _order.AddAsync(newOrder);
            
            cartSubmitted.submitted = true;
            await _cart.UpdateAsync(cartSubmitted);

            v2_ShoppingCart newCart = new v2_ShoppingCart {
                customerId = cartSubmitted.customerId,
                companyId = cartSubmitted.companyId,
                cost = 0.00,
                submitted = false,
                abandoned = false,
                costInString = cartSubmitted.cost.ToString("0.##"),
            };

            await _cart.AddAsync(newCart);

            var i = new JsonResult(paymentSuccess, new JsonSerializerOptions { PropertyNamingPolicy = null});
            return i;
        }

        [HttpPost]
        [Route("submit/takeout/{companyId}/{customerId}")]
        [Consumes("application/x-www-form-urlencoded")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<JsonResult> mobileNewTakeoutOrder([FromRoute] int companyId, string customerId)
        {
            v2_ShoppingCart cartSubmitted = await _cart.getExistingShoppingCart(companyId, customerId);
            v2_UserStripe customerFound = await _IAM.getCustomerDetails(customerId);
            v2_CustomerDTO outgoingDTO = _mapper.Map<v2_CustomerDTO>(customerFound);

            string addressBuilder = $"{customerFound.addressStreet} {customerFound.addressSuite}, {customerFound.addressCity}, {customerFound.addressState} {customerFound.addressPostal_code} {customerFound.addressCountry}";

            v2_Order newOrder = new v2_Order {
                cart = cartSubmitted,
                delivery = true,
                pickedUpByCustomer = false,
                orderCompleted = false,
                deliveryAddress = addressBuilder,
                status = orderReceived,
                eta = defaultETA,
                method = methodPickup,
            };

            _ = await _order.AddAsync(newOrder);
            
            cartSubmitted.submitted = true;
            await _cart.UpdateAsync(cartSubmitted);

            v2_ShoppingCart newCart = new v2_ShoppingCart {
                customerId = cartSubmitted.customerId,
                companyId = cartSubmitted.companyId,
                cost = 0.00,
                submitted = false,
                abandoned = false,
                costInString = cartSubmitted.cost.ToString("0.##"),
            };

            await _cart.AddAsync(newCart);

            var i = new JsonResult(paymentSuccess, new JsonSerializerOptions { PropertyNamingPolicy = null});
            return i;
        }

        [HttpPost]
        [Route("update/address/{customerId}")]
        [Consumes("application/x-www-form-urlencoded")]
        // ?StartIndex={StartIndex}&pagesize={pagesize}&pagenumber={pagenumber}
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        // public async Task<IList<v2_CustomerDTO>> getAllCustomersPaged(int companyId, [FromQuery] QueryParameters QP)
        public async Task<IActionResult> mobileUpdateAddress([FromRoute] string customerId, [FromForm] updateAddressDTO DTO)
        {
            v2_UserStripe found = await _UM.FindByIdAsync(customerId);
            
            found.addressStreet = DTO.addressStreet;
            found.addressSuite = DTO.addressSuite;
            found.addressState = DTO.addressState;
            found.addressCity = DTO.addressCity;
            found.addressCountry = DTO.addressCountry;
            found.addressPostal_code = DTO.addressPostal_code;
            found.PhoneNumber = DTO.PhoneNumber;
            found.latitude = DTO.latitude;
            found.longitude = DTO.longitude;

            await _UM.UpdateAsync(found);
            
            return Ok();
        }

        [HttpPost]
        [Route("get/address/{customerId}")]
        [Consumes("application/x-www-form-urlencoded")]
        // ?StartIndex={StartIndex}&pagesize={pagesize}&pagenumber={pagenumber}
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        // public async Task<IList<v2_CustomerDTO>> getAllCustomersPaged(int companyId, [FromQuery] QueryParameters QP)
        public async Task<updateAddressDTO> mobileGetAddress([FromRoute] string customerId)
        {
            v2_UserStripe found = await _UM.FindByIdAsync(customerId);

            updateAddressDTO outgoingDTO = new updateAddressDTO();
            
            outgoingDTO.addressStreet = found.addressStreet;
            outgoingDTO.addressSuite = found.addressSuite;
            outgoingDTO.addressState = found.addressState;
            outgoingDTO.addressCity = found.addressCity;
            outgoingDTO.addressCountry = found.addressCountry;
            outgoingDTO.addressPostal_code = found.addressPostal_code;
            outgoingDTO.PhoneNumber = found.PhoneNumber;
            outgoingDTO.latitude = found.latitude;
            outgoingDTO.longitude = found.longitude;

            return outgoingDTO;
        }


        [HttpPost]
        [Route("details/{customerId}")]
        [Consumes("application/x-www-form-urlencoded")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<JsonResult> mobileCustomerDetails([FromRoute] string customerId)
        {
            v2_UserStripe customerFound = await _UM.FindByIdAsync(customerId);

            if(customerFound is not null) {

                updateAddressDTO outgoingDTO = new updateAddressDTO();
                    outgoingDTO.addressStreet = customerFound.addressStreet;
                    outgoingDTO.addressSuite = customerFound.addressSuite;
                    outgoingDTO.addressCity = customerFound.addressCity;
                    outgoingDTO.addressState = customerFound.addressState;
                    outgoingDTO.addressPostal_code = customerFound.addressPostal_code;
                    outgoingDTO.addressCountry = customerFound.addressCountry;
                    outgoingDTO.PhoneNumber = customerFound.PhoneNumber;
                    outgoingDTO.longitude = customerFound.longitude;
                    outgoingDTO.latitude = customerFound.latitude;


                var i = new JsonResult(outgoingDTO, new JsonSerializerOptions { PropertyNamingPolicy = null});
                return i;
            } else {
                var i = new JsonResult("", new JsonSerializerOptions { PropertyNamingPolicy = null});
                return i;
            }
        }


        [HttpPost]
        [Route("register")]
        [Consumes("application/x-www-form-urlencoded")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<ActionResult> mobileRegisterNewCustomer([FromForm] Base__APIUser DTO)
        {
            var errors = await _IAM.registerCustomer(DTO);

            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                _LOGS.LogInformation($"Failed Register Attempt for {DTO.Email}");

                return BadRequest(ModelState);
            }

            Stripe.Customer result = await _IAM.addStripeCustomer(DTO.Email);

            return Ok("Success!");
        }

        [HttpGet]
        [Route("allorders/{companyId}/{customerId}")]
        [Consumes("application/x-www-form-urlencoded")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay

        public async Task<JsonResult> mobileGetAllCustomerOrders([FromRoute] int companyId, string customerId)
        {
            IList<v2_Order> allOrdersList = await _order.getAllCustomerOrders(companyId, customerId);
            IList<v2_OrderDTO> listOfAllOrderDTOs = new List<v2_OrderDTO>();

            foreach (v2_Order order in allOrdersList)
            {
                v2_OrderDTO orderBuilder = _mapper.Map<v2_OrderDTO>(order);
                                
                listOfAllOrderDTOs.Add(orderBuilder);
            }

            var i = new JsonResult(listOfAllOrderDTOs, new JsonSerializerOptions { PropertyNamingPolicy = null});
            return i;
        }

        [HttpPost]
        [Route("login")]
        [Consumes("application/x-www-form-urlencoded")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<ActionResult> mobileLoginCustomer([FromForm] Base__APIUser DTO)
        {
            Full__AuthResponseDTO authenticatedUser = await _IAM.loginCustomer(DTO);

            if (authenticatedUser == null)
            {
                _LOGS.LogInformation($"Failed Login Attempt for {DTO.Email}");
                return Unauthorized();
            }

            return Ok(authenticatedUser);
        }
    }
}