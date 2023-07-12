using AuthReadyAPI.DataLayer.DTOs.Pagination;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;

namespace AuthReadyAPI.DataLayer.Repositories
{
    public class v2_StaffRepository : v2_GenericRepository<v2_Staff>, IV2_Staff
    {
        private readonly AuthDbContext _context;
        public v2_StaffRepository(AuthDbContext context) : base(context)
        {
            this._context = context;
        }
    }
}