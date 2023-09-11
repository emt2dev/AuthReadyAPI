using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace AuthReadyAPI.DataLayer.Repositories
{
    public class ShippingOptionRepository : GenericRepository<ShippingOption>, IShippingOption
    {
        private readonly AuthDbContext _context;
        private readonly IMapper _mapper;
        public ShippingOptionRepository(AuthDbContext context, IMapper mapper) : base(context, mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<IList<ShippingOption>> GetAllByDimensions(double CartAreaInInches)
        {
            var matchingProducts = await _context.Set<ShippingOption>()
                 .Where(found => found.AreaInInches <= CartAreaInInches)
                 .ToListAsync();

            return matchingProducts;
        }

        public async Task<IList<ShippingOption>> GetAllByWeight(double CartWeight)
        {
            var matchingProducts = await _context.Set<ShippingOption>()
                 .Where(found => found.MaxWeightInPounds <= CartWeight)
                 .ToListAsync();

            return matchingProducts;
        }
    }
}
