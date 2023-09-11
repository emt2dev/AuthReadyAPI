using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace AuthReadyAPI.DataLayer.Repositories
{
    public class ShippedProductRepository : GenericRepository<ShippedProduct>, IShippedProduct
    {
        private readonly AuthDbContext _context;
        private readonly IMapper _mapper;
        public ShippedProductRepository(AuthDbContext context, IMapper mapper) : base(context, mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<IList<ShippedProduct>> GetAllAvailable(int companyId)
        {
            var matchingProducts = await _context.Set<ShippedProduct>()
                .Where(found => found.CompanyId == companyId.ToString() && found.IsAvailable == false)
                .ToListAsync();

            return matchingProducts;
        }

        public async Task<IList<ShippedProduct>> GetAllUnAvailable(int companyId)
        {
            var matchingProducts = await _context.Set<ShippedProduct>()
                .Where(found => found.CompanyId == companyId.ToString() && found.IsAvailable == false)
                .ToListAsync();

            return matchingProducts;
        }
    }
}
