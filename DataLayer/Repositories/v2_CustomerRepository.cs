using AuthReadyAPI.DataLayer.DTOs.Pagination;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;

namespace AuthReadyAPI.DataLayer.Repositories
{
    public class v2_CustomerRepository : v2_GenericRepository<v2_CustomerStripe>, IV2_Customer
    {
        private readonly AuthDbContext _context;
        public v2_CustomerRepository(AuthDbContext context) : base(context)
        {
            this._context = context;
        }
    }
}