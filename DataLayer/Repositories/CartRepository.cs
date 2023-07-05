using AuthReadyAPI.DataLayer.DTOs.Cart;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AuthReadyAPI.DataLayer.Repositories
{
    public class CartRepository : GenericRepository<Cart>, ICart
    {
        private readonly AuthDbContext _context;
        private readonly IMapper _mapper;

        public CartRepository(AuthDbContext context, IMapper mapper) : base(context, mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<Cart> GET__EXISTING__CART(int companyId, string userId)
        {
            Cart CartFound = await _context.Set<Cart>()
                .Where(found => found.Company == companyId.ToString() && found.Customer == userId)
                .FirstOrDefaultAsync<Cart>(found => found.Submitted == false);

            return CartFound;
        }
    }
}
