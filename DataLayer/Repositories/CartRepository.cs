using AuthReadyAPI.DataLayer.DTOs.PII;
using AuthReadyAPI.DataLayer.DTOs.PII.Payments;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models.PII;
using AuthReadyAPI.DataLayer.Models.ProductInfo;
using AuthReadyAPI.DataLayer.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AuthReadyAPI.DataLayer.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AuthDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediaService _mediaService;
        public CartRepository(AuthDbContext context, IMapper mapper, IMediaService mediaService)
        {
            _mapper = mapper;
            _context = context;
            _mediaService = mediaService;
        }

        public async Task<ShoppingCartDTO> GetUserCart(string UserId)
        {
            ShoppingCartClass Cart = await _context.ShoppingCarts.Where(x => x.UserId == UserId && x.Abandoned == false && x.Submitted == false).Include(x => x.Items).FirstOrDefaultAsync();
            _context.ChangeTracker.Clear();
            if (Cart is null) return null;

            List<CartItemDTO> ItemDTOs = new List<CartItemDTO>();

            double Weight = 0.00;
            double Dimensions = 0.00;
            double ShippingCost = 0.00;
            int PackageCount = 0;

            foreach (var item in Cart.Items)
            {
                CartItemDTO J = _mapper.Map<CartItemDTO>(item);
                ItemDTOs.Add(J);
            }

            List<ShippingInfoDTO> AvailableShipping = await _context.ShippingInfo.ProjectTo<ShippingInfoDTO>(_mapper.ConfigurationProvider).ToListAsync();
            List<ShippingInfoDTO> FlatRateOptions = new List<ShippingInfoDTO>();
            List<ShippingInfoDTO> WeightedOptions = new List<ShippingInfoDTO>();
            List<ShippingInfoDTO> GeneratedOptions = new List<ShippingInfoDTO>();

            foreach (var Box in AvailableShipping)
            {
                double remainingArea = Box.Area;

                foreach (var item in ItemDTOs)
                {
                    _context.ChangeTracker.Clear();
                    StyleDTO S = await _context.Styles.Where(x => x.Id == item.StyleId).ProjectTo<StyleDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();

                    _context.ChangeTracker.Clear();
                    ProductDTO P = await _context.Products.Where(x => x.Id == item.ProductId).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();

                    ProductWithStyleDTO FullProduct = new ProductWithStyleDTO { Product = P, Styles = new List<StyleDTO> { S } };

                    bool fitsInBox = item.PackagedWeight <= Box.MaxWeight && Box.IsWeighed && item.PackagedDimensions <= remainingArea;
                    bool fitsInFlatRateBox = Box.IsFlatRate && item.PackagedDimensions <= remainingArea;

                    if (fitsInBox || fitsInFlatRateBox)
                    {
                        Box.PackingList.Add(FullProduct);
                        ShippingCost += Box.Cost;

                        // Update remaining area in the box
                        remainingArea -= item.PackagedDimensions;

                        // If the remaining area is not enough for the next item, break the loop
                        if (remainingArea <= 0)
                        {
                            break;
                        }
                    }

                    // Add the box to the generated options only if it has items
                    if (Box.PackingList.Any())
                    {
                        GeneratedOptions.Add(Box);
                        PackageCount++;
                    }
                }
            }

            ShoppingCartDTO OutgoingDTO = new ShoppingCartDTO(Cart, ItemDTOs, GeneratedOptions, PackageCount, ShippingCost);
            List<ShippingInfoClass> GeneratedList = new List<ShippingInfoClass>();

            foreach (var item in GeneratedOptions)
            {
                GeneratedList.Add(_mapper.Map<ShippingInfoClass>(item));
            }

            _context.ChangeTracker.Clear();
            await _context.GeneratedShipping.AddRangeAsync(GeneratedList);
            await _context.SaveChangesAsync();

            return OutgoingDTO;
        }

        public async Task<bool> AddItem(AddProductToCartDTO IncomingDTO)
        {
            StyleClass S = await _context.Styles.Where(x => x.Id == IncomingDTO.StyleId).FirstOrDefaultAsync();
            ProductClass P = await _context.Products.Where(x => x.Id == IncomingDTO.ProductId).FirstOrDefaultAsync();
            CartItemClass Item = new CartItemClass
            {
                Id = 0,
                Name = $"{P.Name} {S.Name}",
                ImageUrl = P.MainImageUrl,
                Price = S.CurrentPrice,
                Count = P.Quantity,
                DigitalOnly = S.DigitalOnly,
                StyleId = IncomingDTO.StyleId,
                ProductId = IncomingDTO.ProductId,
                CompanyId = P.CompanyId,
                PackagedDimensions = S.PackagedDimensions,
                PackagedWeight = S.PackagedWeight
            };

            _context.CartItems.Add(Item);
            await _context.SaveChangesAsync();

            _context.ChangeTracker.Clear();
            ShoppingCartClass Cart = await _context.ShoppingCarts.Where(x => x.Id == IncomingDTO.CartId).Include(x => x.Items).FirstOrDefaultAsync();

            Cart.Items.Add(Item);
            _context.ShoppingCarts.Update(Cart);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveItem(RemoveProductFromCartDTO IncomingDTO)
        {
            ShoppingCartClass cart = await _context.ShoppingCarts.Where(x => x.Id == IncomingDTO.CartId).Include(x => x.Items).FirstOrDefaultAsync();

            if (cart != null)
            {
                CartItemClass item = cart.Items.FirstOrDefault(x => x.Id == IncomingDTO.CartItemId);

                if (item != null)
                {
                    item.Count -= IncomingDTO.QuantityReduce;

                    if (item.Count < 1)
                    {

                        _context.CartItems.Remove(item);
                    }
                    else
                    {
                        _context.CartItems.Update(item);
                    }

                    await _context.SaveChangesAsync();
                }

                double NewPrice;
                foreach (var item1 in cart.Items)
                {
                    NewPrice = item1.Count * item1.Price;
                    cart.PriceBeforeCoupon += NewPrice;
                }

                _context.ChangeTracker.Clear();
                _context.ShoppingCarts.Update(cart);
                await _context.SaveChangesAsync();

            }

            return true;
        }

        public async Task<ShoppingCartDTO> IssueNewCart(int CartId)
        {
            ShoppingCartClass Obj = await _context.ShoppingCarts.Where(x => x.Id == CartId).FirstOrDefaultAsync();
            if(Obj is not null)
            {
                Obj.Abandoned = true;

                _context.Update(Obj);
                await _context.SaveChangesAsync();

                ShoppingCartClass New = new ShoppingCartClass
                {
                    Id = 0,
                    Items = new List<CartItemClass>(),
                    Submitted = false,
                    Abandoned = false,
                    CouponApplied = false,
                    PriceAfterCoupon = 0.00,
                    PriceBeforeCoupon = 0.00,
                    CouponCodeId = 0,
                    UserId = Obj.UserId,
                    CompanyId = Obj.CompanyId,
                    Upsells = new List<ProductUpsellItemClass>()
                };

                await _context.AddAsync(New);
                await _context.SaveChangesAsync();

                List<CartItemDTO> ciList = new List<CartItemDTO>();
                List<ShippingInfoDTO> siList = new List<ShippingInfoDTO>();
                ShoppingCartDTO OutgoingDTO = new ShoppingCartDTO(New, ciList, siList, 0, 0.00);

                return OutgoingDTO;
            }
            else
            {
                ShoppingCartClass New = new ShoppingCartClass
                {
                    Id = 0,
                    Items = new List<CartItemClass>(),
                    Submitted = false,
                    Abandoned = false,
                    CouponApplied = false,
                    PriceAfterCoupon = 0.00,
                    PriceBeforeCoupon = 0.00,
                    CouponCodeId = 0,
                    CompanyId = Obj.CompanyId,
                    UserId = Obj.UserId,
                };

                await _context.AddAsync(New);
                await _context.SaveChangesAsync();

                List<CartItemDTO> ciList = new List<CartItemDTO>();
                List<ShippingInfoDTO> siList = new List<ShippingInfoDTO>();
                ShoppingCartDTO OutgoingDTO = new ShoppingCartDTO(New, ciList, siList, 0, 0.00);

                return OutgoingDTO;
            }
        }

        public async Task<ShoppingCartDTO> UpdateCart(ShoppingCartDTO IncomingDTO)
        {
            ShoppingCartClass Obj = _mapper.Map<ShoppingCartClass>(IncomingDTO);

            _context.Update(Obj);
            await _context.SaveChangesAsync();

            List<CartItemDTO> ItemDTOs = new List<CartItemDTO>();

            double Weight = 0.00;
            double Dimensions = 0.00;
            double ShippingCost = 0.00;
            int PackageCount = 0;

            foreach (var item in Obj.Items)
            {
                CartItemDTO J = _mapper.Map<CartItemDTO>(item);
                ItemDTOs.Add(J);
            }

            List<ShippingInfoDTO> AvailableShipping = await _context.ShippingInfo.Where(x => !x.IsDigital).ProjectTo<ShippingInfoDTO>(_mapper.ConfigurationProvider).ToListAsync();
            List<ShippingInfoDTO> FlatRateOptions = new List<ShippingInfoDTO>();
            List<ShippingInfoDTO> WeightedOptions = new List<ShippingInfoDTO>();
            List<ShippingInfoDTO> GeneratedOptions = new List<ShippingInfoDTO>();

            foreach (var Box in AvailableShipping)
            {
                double remainingArea = Box.Area;

                foreach (var item in ItemDTOs)
                {
                    _context.ChangeTracker.Clear();
                    StyleDTO S = await _context.Styles.Where(x => x.Id == item.StyleId).ProjectTo<StyleDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();

                    _context.ChangeTracker.Clear();
                    ProductDTO P = await _context.Products.Where(x => x.Id == item.ProductId).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();

                    ProductWithStyleDTO FullProduct = new ProductWithStyleDTO { Product = P, Styles = new List<StyleDTO> { S } };

                    bool fitsInBox = item.PackagedWeight <= Box.MaxWeight && Box.IsWeighed && item.PackagedDimensions <= remainingArea;
                    bool fitsInFlatRateBox = Box.IsFlatRate && item.PackagedDimensions <= remainingArea;

                    if (fitsInBox || fitsInFlatRateBox)
                    {
                        Box.PackingList.Add(FullProduct);
                        ShippingCost += Box.Cost;

                        // Update remaining area in the box
                        remainingArea -= item.PackagedDimensions;

                        // If the remaining area is not enough for the next item, break the loop
                        if (remainingArea <= 0)
                        {
                            break;
                        }
                    }

                    // Add the box to the generated options only if it has items
                    if (Box.PackingList.Any())
                    {
                        GeneratedOptions.Add(Box);
                        PackageCount++;
                    }
                }
            }

            ShoppingCartDTO OutgoingDTO = new ShoppingCartDTO(Obj, ItemDTOs, GeneratedOptions, PackageCount, ShippingCost);
            return OutgoingDTO;
        }
        /*
        public async Task<bool> AddCustomSale(NewCouponDTO IncomingDTO)
        {
        // Need logic
            
             APIUserClass User = await _UM.FindByEmail(IncomingDTO.UserEmail);
            _context.ChangeTracker.Clear();           

            ShippingInfoClass Box = await _context.ShippingInfo.Where(x => x.Id == IncomingDTO.ShippingId).FirstOrDefaultAsync();

            StyleClass Style = await _context.Styles.Where(x => x.Id == IncomingDTO.StyleId).FirstOrDefaultAsync();
            ProductClass Product = await _context.Products.Where(x => x.Id == Style.ProductId).FirstOrDefaultAsync();
            CartItemClass Item = new CartItemClass
            {
                Id = 0,
                Name = $"{Product.Name} {Style.Name}",
                ImageUrl = Product.MainImageUrl,
                Price = Style.CurrentPrice,
                Count = Product.Quantity,
                DigitalOnly = Style.DigitalOnly,
                StyleId = IncomingDTO.StyleId,
                ProductId = Product.Id,
                CompanyId = Product.CompanyId,
                PackagedDimensions = Style.PackagedDimensions,
                PackagedWeight = Style.PackagedWeight
            };

            _context.CartItems.Add(Item);
            await _context.SaveChangesAsync();

            _context.ChangeTracker.Clear();
            ShoppingCartClass Cart = await _context.ShoppingCarts.Where(x => x.Id == IncomingDTO.CartId).Include(x => x.Items).FirstOrDefaultAsync();

            Cart.Items.Add(Item);
            _context.ShoppingCarts.Update(Cart);
            await _context.SaveChangesAsync();

            return true;
        }
             */
        public async Task<List<SingleProductCartDTO>> GetSingleProductCarts(string UserId)
        {
            List< SingleProductCartDTO> CartList = await _context.SingleProductCarts.Where(x => x.UserId == UserId && x.Abandoned == false && x.Submitted == false).Include(x => x.Item).ProjectTo<SingleProductCartDTO>(_mapper.ConfigurationProvider).ToListAsync();
            _context.ChangeTracker.Clear();
            if (CartList.Count > 1) return null;

            return CartList;
        }
        public async Task<List<AuctionProductCartDTO>> GetAuctionProductCarts(string UserId)
        {
            List<AuctionProductCartDTO> CartList = await _context.AuctionCarts.Where(x => x.UserId == UserId && x.Abandoned == false && x.Submitted == false).Include(x => x.Item).ProjectTo<AuctionProductCartDTO>(_mapper.ConfigurationProvider).ToListAsync();

            _context.ChangeTracker.Clear();
            if (CartList.Count > 1) return null;

            return CartList;
        }

        public async Task<bool> AddSingleProductCart(NewSingleProductDTO IncomingDTO)
        {
            SingleProductClass New = new SingleProductClass(IncomingDTO);
            await _context.SingleProducts.AddAsync(New);
            await _context.SaveChangesAsync();

            _context.ChangeTracker.Clear();

            List<SingleProductImageClass> ImageList = new List<SingleProductImageClass>();

            foreach (var item in IncomingDTO.Images)
            {
                var UploadPhoto = await _mediaService.AddPhotoAsync(item);
                SingleProductImageClass Image = new SingleProductImageClass
                {
                    Id = 0,
                    ImageUrl = UploadPhoto.Url.ToString(),
                    CompanyId = New.CompanyId,
                    SingleProductId = New.Id
                };

                await _context.SingleProductImages.AddAsync(Image);
                await _context.SaveChangesAsync();

                ImageList.Add(Image);
            }

            _context.ChangeTracker.Clear();

            New.AddImages(ImageList);
            _context.SingleProducts.Update(New);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddAuctionProductCart(NewAuctionProductDTO IncomingDTO)
        {
            AuctionProductClass New = new AuctionProductClass(IncomingDTO);
            await _context.AuctionProducts.AddAsync(New);
            await _context.SaveChangesAsync();

            _context.ChangeTracker.Clear();

            List<AuctionProductImageClass> ImageList = new List<AuctionProductImageClass>();

            foreach (var item in IncomingDTO.Images)
            {
                var UploadPhoto = await _mediaService.AddPhotoAsync(item);
                AuctionProductImageClass Image = new AuctionProductImageClass
                {
                    Id = 0,
                    ImageUrl = UploadPhoto.Url.ToString(),
                    CompanyId = New.CompanyId,
                    AuctionProductId = New.Id
                };

                await _context.AuctionProductImages.AddAsync(Image);
                await _context.SaveChangesAsync();

                ImageList.Add(Image);
            }

            _context.ChangeTracker.Clear();

            New.AddImages(ImageList);
            _context.AuctionProducts.Update(New);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
