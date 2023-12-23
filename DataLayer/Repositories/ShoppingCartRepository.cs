using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models.PII;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AuthReadyAPI.DataLayer.Repositories
{
    public class ShoppingCartRepository : IShoppingCart
    {
        private readonly AuthDbContext _context;
        private readonly IMapper _mapper;

        public ShoppingCartRepository(AuthDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public Task<ShoppingCartClass> GET__EXISTING__CART(int companyId, string customerId)
        {
            throw new NotImplementedException();
        }
    }
}
