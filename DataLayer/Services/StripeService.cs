using AuthReadyAPI.DataLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using AuthReadyAPI.DataLayer.Models;
using Microsoft.Extensions.Logging;
using Stripe;
using Stripe.Checkout;
using AuthReadyAPI.DataLayer.DTOs.Product;

namespace AuthReadyAPI.DataLayer.Services
{
    public class StripeService : IStripeService
    {
        private readonly ILogger<StripeService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StripeService(ILogger<StripeService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;            
            _httpContextAccessor = httpContextAccessor;            
        }

        public async Task<string> CheckOut(shoppingCart cart, APIUser customer)
        {

            try
            {
                // Get the base URL 
                var request = _httpContextAccessor.HttpContext!.Request;
                var baseUrl = $"{request.Scheme}://{request.Host}";
                List<SessionLineItemOptions> listOfCartItems = new List<SessionLineItemOptions>();
                
                // Populate LineItems with detail from each ShoppingCart
                foreach (CartItem item in cart.Items)
                {
                    SessionLineItemOptions sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {

                            TaxBehavior = "inclusive",
                            UnitAmount = (long)(item.price * 100),//20.00 -> 2000
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Description = item.description,
                                Name = item.name,
                                TaxCode = "txcd_40060003",
                            },
                        },
                        
                        Quantity = Convert.ToInt64(item.count),
                    };

                    listOfCartItems.Add(sessionLineItem);
                }

                var options = new SessionCreateOptions
                {
                    // Stripe calls these user defined endpoints
                    SuccessUrl = "http://localhost:4200/success",
                    CancelUrl = "http://localhost:4200/cancelled",
                    PaymentMethodTypes = new List<string>
                        {
                            "card",
                        },
                    AutomaticTax = new SessionAutomaticTaxOptions { Enabled = true },
                    Currency = "usd",
                    Mode = "payment",
                    CustomerEmail = customer.Email,
                    ClientReferenceId = cart.customerId.ToString(),
                    LineItems = listOfCartItems,
                    InvoiceCreation = new SessionInvoiceCreationOptions { Enabled = true }
                };
                
                var service = new SessionService();
                var session = await service.CreateAsync(options);

                return session.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError("error into Stripe Service on CheckOut() " + ex.Message);
                throw;
            }

        }

        public async Task<string> v2_CheckOut(v2_ShoppingCart cart, v2_CustomerStripe customer)
        {

            try
            {
                // Get the base URL 
                var request = _httpContextAccessor.HttpContext!.Request;
                var baseUrl = $"{request.Scheme}://{request.Host}";
                List<SessionLineItemOptions> listOfCartItems = new List<SessionLineItemOptions>();
                
                // Populate LineItems with detail from each ShoppingCart
                foreach (v2_ProductStripe item in cart.Items)
                {
                    SessionLineItemOptions sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {

                            TaxBehavior = "inclusive",
                            UnitAmount = (long)(item.default_price * 100),//20.00 -> 2000
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Description = item.description,
                                Name = item.name,
                                TaxCode = "txcd_40060003",
                            },
                        },
                        
                        Quantity = Convert.ToInt64(item.quantity),
                    };

                    listOfCartItems.Add(sessionLineItem);
                }

                var options = new SessionCreateOptions
                {
                    // Stripe calls these user defined endpoints
                    SuccessUrl = "http://localhost:4200/success",
                    CancelUrl = "http://localhost:4200/cancelled",
                    PaymentMethodTypes = new List<string>
                        {
                            "card",
                        },
                    AutomaticTax = new SessionAutomaticTaxOptions { Enabled = true },
                    Currency = "usd",
                    Mode = "payment",
                    CustomerEmail = customer.Email,
                    ClientReferenceId = cart.customerId.ToString(),
                    LineItems = listOfCartItems,
                    InvoiceCreation = new SessionInvoiceCreationOptions { Enabled = true }
                };
                
                var service = new SessionService();
                var session = await service.CreateAsync(options);

                return session.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError("error into Stripe Service on CheckOut() " + ex.Message);
                throw;
            }

        }

        public async Task<string> CheckOut(shoppingCart cart, v2_CustomerStripe customer)
        {

            try
            {
                // Get the base URL 
                var request = _httpContextAccessor.HttpContext!.Request;
                var baseUrl = $"{request.Scheme}://{request.Host}";
                List<SessionLineItemOptions> listOfCartItems = new List<SessionLineItemOptions>();
                
                // Populate LineItems with detail from each ShoppingCart
                foreach (CartItem item in cart.Items)
                {
                    SessionLineItemOptions sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {

                            TaxBehavior = "inclusive",
                            UnitAmount = (long)(item.price * 100),//20.00 -> 2000
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Description = item.description,
                                Name = item.name,
                                TaxCode = "txcd_40060003",
                            },
                        },
                        
                        Quantity = Convert.ToInt64(item.count),
                    };

                    listOfCartItems.Add(sessionLineItem);
                }

                var options = new SessionCreateOptions
                {
                    // Stripe calls these user defined endpoints
                    SuccessUrl = "http://localhost:4200/success",
                    CancelUrl = "http://localhost:4200/cancelled",
                    PaymentMethodTypes = new List<string>
                        {
                            "card",
                        },
                    AutomaticTax = new SessionAutomaticTaxOptions { Enabled = true },
                    Currency = "usd",
                    Mode = "payment",
                    CustomerEmail = customer.Email,
                    ClientReferenceId = cart.customerId.ToString(),
                    LineItems = listOfCartItems,
                    InvoiceCreation = new SessionInvoiceCreationOptions { Enabled = true }
                };
                
                var service = new SessionService();
                var session = await service.CreateAsync(options);

                return session.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError("error into Stripe Service on CheckOut() " + ex.Message);
                throw;
            }

        }

        // public async Task<string> CheckOut(v2_ProductStripe product, long price)
        // {

        //     try
        //     {
        //         // Get the base URL 
        //         var request = _httpContextAccessor.HttpContext!.Request;
        //         var baseUrl = $"{request.Scheme}://{request.Host}";
                
        //         var options = new SessionCreateOptions
        //         {
        //             // Stripe calls these user defined endpoints
        //             SuccessUrl = $"{baseUrl}/payment/success?sessionId=" + "{CHECKOUT_SESSION_ID}",
        //             CancelUrl = baseUrl + "/payment/canceled",

        //             PaymentMethodTypes = new List<string> 
        //             {
        //                 "card"
        //             },
        //             LineItems = new List<SessionLineItemOptions>
        //             {
        //                 new()
        //                 {
        //                     PriceData = new SessionLineItemPriceDataOptions
        //                     {
        //                         UnitAmount = price, // Price defined in RON cents.
        //                         Currency = "USD",
        //                         ProductData = new SessionLineItemPriceDataProductDataOptions
        //                         {
        //                         Name = product.name,
        //                         Description = product.description,
        //                         //Images = new List<string> { product.OrderedTemplate.CoverImgPath }
        //                         },
        //                     },
        //                    Quantity = 1,
        //                 },
        //             },
        //             Mode = "payment", // One time payment
        //             InvoiceCreation = new SessionInvoiceCreationOptions { Enabled = true }
        //         };
                
        //         var service = new SessionService();
        //         var session = await service.CreateAsync(options);

        //         return session.Id;
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError("error into Stripe Service on CheckOut() " + ex.Message);
        //         throw;
        //     }

        // }

    }

}