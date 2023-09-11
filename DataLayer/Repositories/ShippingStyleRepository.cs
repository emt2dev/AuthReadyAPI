using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AutoMapper;

namespace AuthReadyAPI.DataLayer.Repositories
{
    public class ShippingStyleRepository : GenericRepository<ShippedProductStyle>, IShippingStyle
    {
        private readonly AuthDbContext _context;
        private readonly IMapper _mapper;
        public ShippingStyleRepository(AuthDbContext context, IMapper mapper) : base(context, mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
    }
}
