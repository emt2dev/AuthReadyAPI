using AuthReadyAPI.DataLayer.DTOs.PII.APIUser;
using AuthReadyAPI.DataLayer.DTOs.PII.Payments;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models.PII;
using AuthReadyAPI.DataLayer.Models.ProductInfo;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Issuing;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace AuthReadyAPI.DataLayer.Repositories
{
    public class AuctionRepository
    {
        private readonly AuthDbContext _context;
        private readonly IMediaService _mediaService;
        private readonly IMapper _mapper;
        public AuctionRepository(AuthDbContext context, IMapper mapper, IMediaService mediaService)
        {
            _context = context;
            _mediaService = mediaService;
            _mapper = mapper;
        }

        public async Task<List<AuctionProductDTO>> GetActiveAuctionProducts()
        {
            return await _context.AuctionProducts.Where(x => x.AuctionEnd < DateTime.Now).ProjectTo<AuctionProductDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<bool> AddBid(BidDTO IncomingDTO)
        {
            // APIUser User = await _userManager.FindById(IncomingDTO.UserId);
            _context.ChangeTracker.Clear();

            AuctionProductClass Product = await _context.AuctionProducts.Where(x => x.Id == IncomingDTO.AuctionProductId).FirstOrDefaultAsync();
            if (Product is null || Product.AuctionEnd < DateTime.Now) return false;

            _context.ChangeTracker.Clear();

            BidClass Bid = _mapper.Map<BidClass>(IncomingDTO);
            Bid.Submitted = DateTime.Now;

            await _context.Bids.AddAsync(Bid);
            await _context.SaveChangesAsync();

            _context.ChangeTracker.Clear();

            Product.Bids.Add(Bid);
            if(Product.CurrentBidAmount < Bid.Amount) Product.CurrentBidAmount = Bid.Amount;
            if(Product.AutoSellBidAmount < Bid.Amount && Product.AcceptAutoSell)
            {
                Product.AuctionEnd = DateTime.Now;
                Product.CurrentBidderId = IncomingDTO.UserId;
            }

            _context.AuctionProducts.Update(Product);
            await _context.SaveChangesAsync();

            AuctionProductCartClass Cart = new AuctionProductCartClass
            {
                Id = 0,
                Item = Product,
                UserId = IncomingDTO.UserId,
                Submitted = false,
                Abandoned = false,
                Expiration = DateTime.Now.AddHours(24).ToString("MM/dd/yyyy"),
                Upsells = new List<ProductUpsellItemClass>()
            };

            await _context.AuctionCarts.AddAsync(Cart);
            await _context.SaveChangesAsync();

            return false;
        }

        public async Task<bool> AddAuctionProduct(NewAuctionProductDTO IncomingDTO)
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

        public async Task<List<AuctionProductCartDTO>> GetFinishedAuctionsByCompanyId(int CompanyId)
        {
            List<AuctionProductClass> Auctions = await _context.AuctionProducts.Where(x => x.CompanyId == CompanyId && x.AuctionEnd < DateTime.Now && !x.HasBeenPurchased).ToListAsync();
            _context.ChangeTracker.Clear();

            List<AuctionProductCartDTO> ListOfCarts = new List<AuctionProductCartDTO>();
            foreach (var item in Auctions)
            {
                BidClass Winner = item.Bids.OrderByDescending(x => x.Amount).FirstOrDefault();
                _context.ChangeTracker.Clear();

                // APIUser User = await _userManager.FindById(Winner.UserId)

                _context.ChangeTracker.Clear();
                AuctionProductCartClass Cart = new AuctionProductCartClass
                {
                    Id = 0,
                    Item = item,
                    UserId = Winner.UserId,
                    Submitted = false,
                    Abandoned = false,
                    Expiration = DateTime.Now.AddHours(24).ToString("MM/dd/yyyy"),
                    Upsells = new List<ProductUpsellItemClass>(),
                    CompanyId = CompanyId,
                };

                await _context.AuctionCarts.AddAsync(Cart);
                await _context.SaveChangesAsync();

                AuctionProductCartDTO OutgoingDTO = _mapper.Map<AuctionProductCartDTO>(Cart);
                OutgoingDTO.UserId = "User Id Hidden, refer to User Email";
                //OutgoingDTO.UserEmail = Winner.Email;

                ListOfCarts.Add(OutgoingDTO);
            }

            _context.ChangeTracker.Clear();
            List<AuctionProductCartClass> CurrentCarts = await _context.AuctionCarts.Where(x => x.CompanyId == CompanyId && x.Submitted == false).ToListAsync();
            _context.ChangeTracker.Clear();

            foreach (var item in CurrentCarts)
            {
                if (item.HasExpired())
                {
                    item.Abandoned = true;
                    _context.AuctionCarts.Update(item);
                    await _context.SaveChangesAsync();

                    _context.ChangeTracker.Clear();
                    ShoppingCartClass Cart = await _context.ShoppingCarts.Where(x => x.UserId == item.UserId && !x.Submitted && !x.Abandoned).Include(x => x.Items).FirstOrDefaultAsync();

                    // Add penalty fee for abandonded cart here
                    Cart.PriceAfterCoupon = Cart.PriceBeforeCoupon += item.Item.CurrentBidAmount / 2; // We charge the user 50% of the bid amount.
                }
                else
                {
                    AuctionProductCartDTO OutgoingDTO = _mapper.Map<AuctionProductCartDTO>(item);
                    OutgoingDTO.UserId = "User Id Hidden, refer to User Email";
                    //OutgoingDTO.UserEmail = Winner.Email;
                    ListOfCarts.Add(OutgoingDTO);
                }
            }

            return ListOfCarts;
        }

        public async Task<List<AuctionProductCartDTO>> GetFinishAuctionsByUserId(string UserId)
        {
            // verify User exists
            // APIUser User = await _userManager.FindById(UserId);
            // _context.ChangeTracker.Clear();

            List<AuctionProductCartDTO> OutgoingList = new List<AuctionProductCartDTO>();

            List<AuctionProductCartClass> CartList = await _context.AuctionCarts.Where(x => x.UserId ==  UserId && !x.Submitted && !x.Abandoned).ToListAsync();
            foreach (var item in CartList)
            {
                var C = _mapper.Map<AuctionProductCartDTO>(item);
                C.UserId = "User Id Hidden, refer to User Email";
                // C.UserEmail = User.Email

                OutgoingList.Add(C);
            }

            return OutgoingList;
        }
    }
}
