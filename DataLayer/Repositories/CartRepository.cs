using AuthReadyAPI.DataLayer.DTOs.PII;
using AuthReadyAPI.DataLayer.DTOs.PII.Payments;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models.PII;
using AuthReadyAPI.DataLayer.Models.ProductInfo;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AuthReadyAPI.DataLayer.Repositories
{
    public class CartRepository : ICart
    {
        private readonly AuthDbContext _context;
        private readonly IMapper _mapper;
        public CartRepository(AuthDbContext context, IMapper mapper)
        {
            this._mapper = mapper;
            this._context = context;
        }

        public async Task<ShoppingCartDTO> GetUserCart(string UserId)
        {
            ShoppingCartClass Cart = await _context.ShoppingCarts.Where(x => x.UserId == UserId).Include(x => x.Items).FirstOrDefaultAsync();
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

                    ProductWithStyleDTO FullProduct = new ProductWithStyleDTO(P, S);

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

            // Ensure the cart exists
            if (cart != null)
            {
                CartItemClass item = cart.Items.FirstOrDefault(x => x.Id == IncomingDTO.CartItemId);

                if (item != null)
                {
                    item.Count -= IncomingDTO.QuantityReduce;

                    if (item.Count < 1)
                    {
                        _context.Remove(item);
                    }
                    else
                    {
                        _context.Update(item);
                    }

                    await _context.SaveChangesAsync();
                }
            }

            return true;
        }
    }
}
