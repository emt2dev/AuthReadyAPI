using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models.Companies;
using AuthReadyAPI.DataLayer.Models.PII;
using AuthReadyAPI.DataLayer.Models.ProductInfo;
using AuthReadyAPI.DataLayer.Services;
using AutoMapper;
using Azure;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;
using System.Text;

namespace AuthReadyAPI.DataLayer.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AuthDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediaService _mediaService;
        private readonly UserManager<APIUserClass> _userManager;
        private APIUserClass _user;
        private readonly ICartRepository _cart;

        public OrderRepository(AuthDbContext context, IMapper mapper, IMediaService mediaService, UserManager<APIUserClass> userManager, ICartRepository cart)
        {
            _mapper = mapper;
            _context = context;
            _mediaService = mediaService;
            _userManager = userManager;
            _cart = cart;
        }

        public async Task<bool> FinalizeSale(int PreparedCartId)
        {
            PreparedCartClass Cart = await _context.PreparedCarts.Where(x => x.Id == PreparedCartId).FirstOrDefaultAsync();
            if (Cart is null) return false;

            OrderClass NewOrder = new OrderClass
            {
                Id = 0,
                Status = string.Empty,
                PaymentAmount = 0.00,
                PaymentCompleted = true,
                PaymentRefunded = false,
                UserEmail = "Email Not Available",
                CartType = Cart.CartType,
                SingleProductCartClassId = 0,
                ShoppingCartId = 0,
                AuctionProductCartClassId = 0,
                ServicesCartClassId = 0,
                CompanyId = Cart.CompanyId,
                ShippingInfos = new List<ShippingInfoClass>(),
                DigitalOwnerships = new List<DigitalOwnershipClass>()
            };

            switch (Cart.CartType.ToLower())
            {
                case "single":
                    SingleProductCartClass Scart = await _context.SingleProductCarts.Where(x => x.Id == Cart.CartId).FirstOrDefaultAsync();
                    Scart.Submitted = true;
                    Scart.Abandoned = false;
                    _context.SingleProductCarts.Update(Scart);
                    await _context.SaveChangesAsync();

                    _context.ChangeTracker.Clear();
                    _user = await _userManager.FindByIdAsync(Scart.UserId);

                    if (_user is not null) NewOrder.UserEmail = _user.Email;
                    _context.ChangeTracker.Clear();

                    NewOrder.SingleProductCartClassId = Scart.Id;

                    foreach (var item in Scart.Upsells)
                    {
                        NewOrder.PaymentAmount += item.CostWithShipping * item.Quantity;
                    }

                    NewOrder.PaymentAmount += Scart.Item.ProductPrice;

                    ShippingInfoClass NewSingleShipping = new ShippingInfoClass
                    {
                        Id = 0,
                        Name = "Customer Shipping",
                        Carrier = Scart.Item.Carrier,
                        Cost = Scart.Item.ShippingCost,
                        Area = 0.00,
                        MaxWeight = 0.00,
                        IsAvailable = false,
                        IsWeighed = false,
                        IsDigital = false,
                        DeliveryExpectation = Scart.Item.Delivery,
                        TrackingNumber = "Not Yet Added",
                        PackingList = new List<ProductWithStyleClass>(),
                    };

                    _context.ChangeTracker.Clear();
                    await _context.ShippingInfo.AddAsync(NewSingleShipping);
                    await _context.SaveChangesAsync();

                    _context.ChangeTracker.Clear();
                    NewOrder.ShippingInfos.Add(NewSingleShipping);
                    NewOrder.Status = "Awaiting Tracking Number"; // may add this to be completd automatically

                    await _context.Orders.AddAsync(NewOrder);
                    await _context.SaveChangesAsync();

                    CompanyTransactionClass STransaction = new CompanyTransactionClass
                    {
                        Id = 0,
                        TimeOfTransaction = DateTime.Now,
                        SaleGross = NewOrder.PaymentAmount,
                        CompanyNet = NewOrder.PaymentAmount * .80,
                        AuthReadyNet = NewOrder.PaymentAmount * .20,
                        DepositedToCompany = false,
                        DepositTime = DateTime.Now,
                        TransactionType = NewOrder.CartType,
                        TransactionTypeId = NewOrder.SingleProductCartClassId,
                        UserEmail = NewOrder.UserEmail,
                        CompanyId = NewOrder.CompanyId,
                        OrderId = NewOrder.Id
                    };

                    _context.ChangeTracker.Clear();
                    await _context.CompanyTransactions.AddAsync(STransaction);
                    await _context.SaveChangesAsync();

                    return true;

                case "auction":
                    AuctionProductCartClass Acart = await _context.AuctionCarts.Where(x => x.Id == Cart.CartId).FirstOrDefaultAsync();
                    Acart.Submitted = true;
                    Acart.Abandoned = false;
                    _context.AuctionCarts.Update(Acart);
                    await _context.SaveChangesAsync();

                    _context.ChangeTracker.Clear();
                    _user = await _userManager.FindByIdAsync(Acart.UserId);

                    if (_user is not null) NewOrder.UserEmail = _user.Email;
                    _context.ChangeTracker.Clear();

                    NewOrder.AuctionProductCartClassId = Acart.Id;

                    foreach (var item in Acart.Upsells)
                    {
                        NewOrder.PaymentAmount += item.CostWithShipping * item.Quantity;
                    }

                    NewOrder.PaymentAmount += Acart.Item.CurrentBidAmount;

                    ShippingInfoClass NewShipping = new ShippingInfoClass
                    {
                        Id = 0,
                        Name = "Customer Shipping",
                        Carrier = Acart.Item.Carrier,
                        Cost = Acart.Item.ShippingCost,
                        Area = 0.00,
                        MaxWeight = 0.00,
                        IsAvailable = false,
                        IsWeighed = false,
                        IsDigital = false,
                        DeliveryExpectation = Acart.Item.Delivery,
                        TrackingNumber = "Not Yet Added",
                        PackingList = new List<ProductWithStyleClass>(),
                    };

                    _context.ChangeTracker.Clear();
                    await _context.ShippingInfo.AddAsync(NewShipping);
                    await _context.SaveChangesAsync();

                    _context.ChangeTracker.Clear();
                    NewOrder.ShippingInfos.Add(NewShipping);
                    NewOrder.Status = "Awaiting Tracking Number"; // may add this to be completd automatically

                    await _context.Orders.AddAsync(NewOrder);
                    await _context.SaveChangesAsync();

                    CompanyTransactionClass ATransaction = new CompanyTransactionClass
                    {
                        Id = 0,
                        TimeOfTransaction = DateTime.Now,
                        SaleGross = NewOrder.PaymentAmount,
                        CompanyNet = NewOrder.PaymentAmount * .80,
                        AuthReadyNet = NewOrder.PaymentAmount * .20,
                        DepositedToCompany = false,
                        DepositTime = DateTime.Now,
                        TransactionType = NewOrder.CartType,
                        TransactionTypeId = NewOrder.AuctionProductCartClassId,
                        UserEmail = NewOrder.UserEmail,
                        CompanyId = NewOrder.CompanyId,
                        OrderId = NewOrder.Id
                    };

                    _context.ChangeTracker.Clear();
                    await _context.CompanyTransactions.AddAsync(ATransaction);
                    await _context.SaveChangesAsync();

                    return true;

                case "shopping":
                    ShoppingCartClass FullCart = await _context.ShoppingCarts.Where(x => x.Id == Cart.CartId).FirstOrDefaultAsync();
                    FullCart.Submitted = true;
                    FullCart.Abandoned = false;
                    _context.ShoppingCarts.Update(FullCart);
                    await _context.SaveChangesAsync();

                    _ = await _cart.IssueNewCart(FullCart.Id);

                    _context.ChangeTracker.Clear();
                    _user = await _userManager.FindByIdAsync(FullCart.UserId);

                    if(_user is not null) NewOrder.UserEmail = _user.Email;

                    _context.ChangeTracker.Clear();

                    NewOrder.ShoppingCartId = FullCart.Id;
                    NewOrder.PaymentAmount += FullCart.PriceAfterCoupon;

                    _context.ChangeTracker.Clear();
                    List<ShippingInfoClass> GenShiping = await _context.GeneratedShipping.Where(x => x.CartId == FullCart.Id).ToListAsync();

                    _context.ChangeTracker.Clear();
                    NewOrder.ShippingInfos.AddRange(GenShiping);
                    NewOrder.Status = "Awaiting Tracking Number"; // may add this to be completd automatically

                    await _context.Orders.AddAsync(NewOrder);
                    await _context.SaveChangesAsync();

                    CompanyTransactionClass FTransaction = new CompanyTransactionClass
                    {
                        Id = 0,
                        TimeOfTransaction = DateTime.Now,
                        SaleGross = NewOrder.PaymentAmount,
                        CompanyNet = NewOrder.PaymentAmount * .80,
                        AuthReadyNet = NewOrder.PaymentAmount * .20,
                        DepositedToCompany = false,
                        DepositTime = DateTime.Now,
                        TransactionType = NewOrder.CartType,
                        TransactionTypeId = NewOrder.ShoppingCartId,
                        UserEmail = NewOrder.UserEmail,
                        CompanyId = NewOrder.CompanyId,
                        OrderId = NewOrder.Id
                    };

                    _context.ChangeTracker.Clear();
                    await _context.CompanyTransactions.AddAsync(FTransaction);
                    await _context.SaveChangesAsync();


                    return true;

                case "services":
                    ServicesCartClass SvcCart = await _context.ServiceCarts.Where(x => x.Id == Cart.CartId).FirstOrDefaultAsync();
                    SvcCart.Submitted = true;
                    SvcCart.Abandoned = false;
                    _context.ServiceCarts.Update(SvcCart);
                    await _context.SaveChangesAsync();

                    _context.ChangeTracker.Clear();
                    _user = await _userManager.FindByEmailAsync(SvcCart.UserEmail);

                    if (_user is not null) NewOrder.UserEmail = _user.Email;
                    _context.ChangeTracker.Clear();

                    NewOrder.ServicesCartClassId = SvcCart.Id;
                    NewOrder.PaymentAmount = SvcCart.PriceAfterCoupon;

                    _context.ChangeTracker.Clear();
                    await _context.Orders.AddAsync(NewOrder);
                    await _context.SaveChangesAsync();

                    CompanyTransactionClass SvcTransaction = new CompanyTransactionClass
                    {
                        Id = 0,
                        TimeOfTransaction = DateTime.Now,
                        SaleGross = NewOrder.PaymentAmount,
                        CompanyNet = NewOrder.PaymentAmount * .80,
                        AuthReadyNet = NewOrder.PaymentAmount * .20,
                        DepositedToCompany = false,
                        DepositTime = DateTime.Now,
                        TransactionType = NewOrder.CartType,
                        TransactionTypeId = NewOrder.ServicesCartClassId,
                        UserEmail = NewOrder.UserEmail,
                        CompanyId = NewOrder.CompanyId,
                        OrderId = NewOrder.Id
                    };

                    _context.ChangeTracker.Clear();
                    await _context.CompanyTransactions.AddAsync(SvcTransaction);
                    await _context.SaveChangesAsync();

                    return true;

                default:
                    return false;
            }
        }

        public async Task<string> PrepareCart(string CartType, int CartId)
        {
            StringBuilder Url = new StringBuilder();

            switch (CartType)
            {
                case "single":
                    SingleProductCartClass SPCart = await _context.SingleProductCarts.Where(x => x.Id == CartId).FirstOrDefaultAsync();
                    _context.ChangeTracker.Clear();

                    _user = await _userManager.FindByIdAsync(SPCart.UserId);
                    if (_user is null) return string.Empty;

                    _context.ChangeTracker.Clear();

                    // Begin Stripe Here using Stripe Checkout
                    var SPCartoptions = new SessionCreateOptions
                    {
                        PaymentMethodTypes = new List<string>
                        {
                            "card",
                        },
                        AutomaticTax = new SessionAutomaticTaxOptions { Enabled = true },
                        LineItems = new List<SessionLineItemOptions>(),
                        Currency = "usd",
                        Mode = "payment",
                        SuccessUrl = "http://localhost:4200/success.html",
                        CancelUrl = "http://localhost:4200/cancel.html",
                        CustomerEmail = _user.Email,
                        ClientReferenceId = SPCart.Id.ToString(),
                    };

                    // Populate LineItems with detail from each ShoppingCart
                    SessionLineItemOptions SPCartsessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {

                            TaxBehavior = "inclusive",
                            UnitAmount = (long)(SPCart.Item.TotalCost * 100), //20.00 -> 2000
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Description = SPCart.Item.Description,
                                Name = SPCart.Item.Name,
                                TaxCode = SPCart.Item.TaxCode,
                            },
                        },

                        Quantity = 1
                    };
                    SPCartoptions.LineItems.Add(SPCartsessionLineItem);

                    foreach (var item in SPCart.Upsells)
                    {
                        SessionLineItemOptions UpsellACartsessionLineItem = new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {

                                TaxBehavior = "inclusive",
                                UnitAmount = (long)(item.CostWithShipping * 100), //20.00 -> 2000
                                Currency = "usd",
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Description = item.Description,
                                    Name = item.Name,
                                    TaxCode = item.TaxCode,
                                },
                            },

                            Quantity = 1
                        };

                        SPCartoptions.LineItems.Add(UpsellACartsessionLineItem);
                    }

                    PreparedCartClass SPPrep = new PreparedCartClass
                    {
                        Id = 0,
                        CartId = SPCart.Id,
                        CartType = "single",
                        CompanyId = SPCart.CompanyId
                    };

                    await _context.PreparedCarts.AddAsync(SPPrep);
                    await _context.SaveChangesAsync();

                    // Create a Stripe API session service
                    var SPCartservice = new SessionService();

                    // Use the session service to create a Stripe API session
                    Session SPCartsession = SPCartservice.Create(SPCartoptions);
                    Url.Append(SPCartsession.Url);

                    return Url.ToString();

                case "auction":
                    AuctionProductCartClass ACart = await _context.AuctionCarts.Where(x => x.Id == CartId).FirstOrDefaultAsync();
                    _context.ChangeTracker.Clear();

                    _user = await _userManager.FindByIdAsync(ACart.UserId);
                    if (_user is null) return string.Empty;

                    _context.ChangeTracker.Clear();

                    // Begin Stripe Here using Stripe Checkout
                    var ACartoptions = new SessionCreateOptions
                    {
                        PaymentMethodTypes = new List<string>
                        {
                            "card",
                        },
                        AutomaticTax = new SessionAutomaticTaxOptions { Enabled = true },
                        LineItems = new List<SessionLineItemOptions>(),
                        Currency = "usd",
                        Mode = "payment",
                        SuccessUrl = "http://localhost:4200/success.html",
                        CancelUrl = "http://localhost:4200/cancel.html",
                        CustomerEmail = _user.Email,
                        ClientReferenceId = ACart.Id.ToString(),
                    };

                    // Populate LineItems with detail from each ShoppingCart
                    SessionLineItemOptions ACartsessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {

                            TaxBehavior = "inclusive",
                            UnitAmount = (long)((ACart.Item.CurrentBidAmount + ACart.Item.ShippingCost) * 100), //20.00 -> 2000
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Description = ACart.Item.Description,
                                Name = ACart.Item.Name,
                                TaxCode = ACart.Item.TaxCode,
                            },
                        },

                        Quantity = 1
                    };

                    ACartoptions.LineItems.Add(ACartsessionLineItem);

                    foreach (var item in ACart.Upsells)
                    {
                        SessionLineItemOptions UpsellACartsessionLineItem = new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {

                                TaxBehavior = "inclusive",
                                UnitAmount = (long)(item.CostWithShipping * 100), //20.00 -> 2000
                                Currency = "usd",
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Description = item.Description,
                                    Name = item.Name,
                                    TaxCode = item.TaxCode,
                                },
                            },

                            Quantity = 1
                        };

                        ACartoptions.LineItems.Add(UpsellACartsessionLineItem);
                    }

                    PreparedCartClass ACPrep = new PreparedCartClass
                    {
                        Id = 0,
                        CartId = ACart.Id,
                        CartType = "auction",
                        CompanyId = ACart.CompanyId
                    };

                    await _context.PreparedCarts.AddAsync(ACPrep);
                    await _context.SaveChangesAsync();

                    // Create a Stripe API session service
                    var ACartservice = new SessionService();

                    // Use the session service to create a Stripe API session
                    Session ACartsession = ACartservice.Create(ACartoptions);
                    Url.Append(ACartsession.Url);

                    return Url.ToString();

                case "shopping":
                    ShoppingCartClass SCart = await _context.ShoppingCarts.Where(x => x.Id == CartId).FirstOrDefaultAsync();
                    _context.ChangeTracker.Clear();

                    _user = await _userManager.FindByIdAsync(SCart.UserId);
                    if (_user is null) return string.Empty;

                    _context.ChangeTracker.Clear();

                    // Begin Stripe Here using Stripe Checkout
                    var SCartoptions = new SessionCreateOptions
                    {
                        PaymentMethodTypes = new List<string>
                        {
                            "card",
                        },
                        AutomaticTax = new SessionAutomaticTaxOptions { Enabled = true },
                        LineItems = new List<SessionLineItemOptions>(),
                        Currency = "usd",
                        Mode = "payment",
                        SuccessUrl = "http://localhost:4200/success.html",
                        CancelUrl = "http://localhost:4200/cancel.html",
                        CustomerEmail = _user.Email,
                        ClientReferenceId = SCart.Id.ToString(),
                    };

                    // Populate LineItems with detail from each ShoppingCart
                    foreach (var item in SCart.Items)
                    {
                        SessionLineItemOptions SCartsessionLineItem = new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {

                                TaxBehavior = "inclusive",
                                UnitAmount = (long)(item.Price * 100), //20.00 -> 2000
                                Currency = "usd",
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Description = item.Description,
                                    Name = item.Name,
                                    TaxCode = item.TaxCode,
                                },
                            },

                            Quantity = 1
                        };

                        SCartoptions.LineItems.Add(SCartsessionLineItem);
                    }

                    foreach (var item in SCart.Upsells)
                    {
                        SessionLineItemOptions UpsellSCartsessionLineItem = new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {

                                TaxBehavior = "inclusive",
                                UnitAmount = (long)(item.CostWithShipping * 100), //20.00 -> 2000
                                Currency = "usd",
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Description = item.Description,
                                    Name = item.Name,
                                    TaxCode = item.TaxCode,
                                },
                            },

                            Quantity = 1
                        };

                        SCartoptions.LineItems.Add(UpsellSCartsessionLineItem);
                    }

                    PreparedCartClass SCPrep = new PreparedCartClass
                    {
                        Id = 0,
                        CartId = SCart.Id,
                        CartType = "shopping",
                        CompanyId = SCart.CompanyId
                    };

                    await _context.PreparedCarts.AddAsync(SCPrep);
                    await _context.SaveChangesAsync();

                    // Create a Stripe API session service
                    var SCartservice = new SessionService();

                    // Use the session service to create a Stripe API session
                    Session SCartsession = SCartservice.Create(SCartoptions);
                    Url.Append(SCartsession.Url);

                    return Url.ToString();

                case "services":
                    ServicesCartClass SvcCart = await _context.ServiceCarts.Where(x => x.Id == CartId).FirstOrDefaultAsync();
                    _context.ChangeTracker.Clear();

                    _user = await _userManager.FindByEmailAsync(SvcCart.UserEmail);
                    if (_user is null) return string.Empty;

                    _context.ChangeTracker.Clear();

                    // Begin Stripe Here using Stripe Checkout
                    var SvcCartoptions = new SessionCreateOptions
                    {
                        PaymentMethodTypes = new List<string>
                        {
                            "card",
                        },
                        AutomaticTax = new SessionAutomaticTaxOptions { Enabled = true },
                        LineItems = new List<SessionLineItemOptions>(),
                        Currency = "usd",
                        Mode = "payment",
                        SuccessUrl = "http://localhost:4200/success.html",
                        CancelUrl = "http://localhost:4200/cancel.html",
                        CustomerEmail = _user.Email,
                        ClientReferenceId = SvcCart.Id.ToString(),
                    };

                    // Populate LineItems with detail from each ShoppingCart
                    foreach (var Appt in SvcCart.Appointments)
                    {
                        foreach (var item in Appt.Products)
                        {
                            SessionLineItemOptions SvcCartsessionLineItem = new SessionLineItemOptions
                            {
                                PriceData = new SessionLineItemPriceDataOptions
                                {

                                    TaxBehavior = "inclusive",
                                    UnitAmount = (long)(item.Cost * 100), //20.00 -> 2000
                                    Currency = "usd",
                                    ProductData = new SessionLineItemPriceDataProductDataOptions
                                    {
                                        Description = item.Description,
                                        Name = item.Name,
                                        TaxCode = item.TaxCode,
                                    },
                                },

                                Quantity = 1
                            };

                            SvcCartoptions.LineItems.Add(SvcCartsessionLineItem);
                        }

                        foreach (var item in Appt.Services)
                        {
                            SessionLineItemOptions ServicesSvcCartsessionLineItem = new SessionLineItemOptions
                            {
                                PriceData = new SessionLineItemPriceDataOptions
                                {

                                    TaxBehavior = "inclusive",
                                    UnitAmount = (long)(item.CurrentPrice * 100), //20.00 -> 2000
                                    Currency = "usd",
                                    ProductData = new SessionLineItemPriceDataProductDataOptions
                                    {
                                        Description = item.Description,
                                        Name = item.Name,
                                        TaxCode = item.TaxCode,
                                    },
                                },

                                Quantity = 1
                            };

                            SvcCartoptions.LineItems.Add(ServicesSvcCartsessionLineItem);
                        }
                    }

                    PreparedCartClass SvcPrep = new PreparedCartClass
                    {
                        Id = 0,
                        CartId = SvcCart.Id,
                        CartType = "services",
                        CompanyId = SvcCart.CompanyId
                    };

                    await _context.PreparedCarts.AddAsync(SvcPrep);
                    await _context.SaveChangesAsync();

                    // Create a Stripe API session service
                    var SvcCartservice = new SessionService();

                    // Use the session service to create a Stripe API session
                    Session SvcCartsession = SvcCartservice.Create(SvcCartoptions);
                    Url.Append(SvcCartsession.Url);

                    return Url.ToString();

                default:
                    return string.Empty;
            }
        }
    }
}
