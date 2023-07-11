using AuthReadyAPI.DataLayer.DTOs.Pagination;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;

namespace AuthReadyAPI.DataLayer.Repositories
{
    public class v2_ProductRepository : v2_GenericRepository<v2_ProductStripe>, IV2_Product
    {
        private readonly AuthDbContext _context;
        public v2_ProductRepository(AuthDbContext context) : base(context)
        {
            this._context = context;
        }
    }
}