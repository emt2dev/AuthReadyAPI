using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AuthReadyAPI.DataLayer.Repositories
{
    public class ShoppingCartRepository : GenericRepository<shoppingCart>, IShoppingCart
    {
        private readonly AuthDbContext _context;
        private readonly IMapper _mapper;

        public ShoppingCartRepository(AuthDbContext context, IMapper mapper) : base(context, mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<shoppingCart> GET__EXISTING__CART(int companyId, string customerId)
        {
            shoppingCart shoppingCartFound = await _context.shoppingCarts.Include(bag => bag.Items)
                .Where(found => found.companyId == companyId.ToString() && found.customerId == customerId)
                // .FirstOrDefaultAsync<shoppingCart>(found => found.submitted == false && found.abandoned == false);
                .FirstOrDefaultAsync<shoppingCart>(found => found.submitted == false && found.abandoned == false);

            return shoppingCartFound;
        }
    }
}
