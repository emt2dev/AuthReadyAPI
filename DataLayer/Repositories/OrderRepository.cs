using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models.PII;
using AutoMapper;

namespace AuthReadyAPI.DataLayer.Repositories
{
    public class OrderRepository : IOrder
    {
        private readonly AuthDbContext _context;
        private readonly IMapper _mapper;

        public OrderRepository(AuthDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
    }
}
