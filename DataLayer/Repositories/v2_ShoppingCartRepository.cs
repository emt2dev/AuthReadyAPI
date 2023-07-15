using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AuthReadyAPI.DataLayer.Repositories
{
    public class v2_ShoppingCartRepository : v2_GenericRepository<v2_ShoppingCart>, IV2_ShoppingCart
    {
        private readonly AuthDbContext _context;

        public v2_ShoppingCartRepository(AuthDbContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<v2_ShoppingCart> getCart(int cartId)
        {
            v2_ShoppingCart cart = await _context.Set<v2_ShoppingCart>()
            .Include(x => x.Items)
            .FirstOrDefaultAsync<v2_ShoppingCart>(x => x.Id == cartId);

            return cart;
        }

        public async Task<v2_ShoppingCart> getExistingShoppingCart(int companyId, string customerId)
        {
            v2_ShoppingCart shoppingCartFound = await _context.v2_ShoppingCarts.Include(bag => bag.Items)
                .Where(found => found.companyId == companyId.ToString() && found.customerId == customerId)
                .FirstOrDefaultAsync<v2_ShoppingCart>(found => found.submitted == false && found.abandoned == false);

            return shoppingCartFound;
        }
    }
}
