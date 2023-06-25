using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AutoMapper;

namespace AuthReadyAPI.DataLayer.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrder
    {
        private readonly AuthDbContext _context;
        private readonly IMapper _mapper;

        public OrderRepository(AuthDbContext context, IMapper mapper) : base(context, mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
    }
}
