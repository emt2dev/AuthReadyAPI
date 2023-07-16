using AuthReadyAPI.DataLayer.DTOs.Pagination;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthReadyAPI.DataLayer.Repositories
{
    public class v2_UserRepository : v2_GenericRepository<v2_UserStripe>, IV2_User
    {
        private readonly AuthDbContext _context;
        public v2_UserRepository(AuthDbContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<IList<v2_UserStripe>> getAllStaff(int companyId)
        {
            IList<v2_UserStripe> listOfAllStaff = await _context.Set<v2_UserStripe>()
            .Where(found => found.companyId == companyId)
            .ToListAsync();

            return listOfAllStaff;
        }

        public async Task<IList<v2_UserStripe>> getAdmins(int companyId)
        {
            IList<v2_UserStripe> listOfAllStaff = await _context.Set<v2_UserStripe>()
            .Where(found => found.companyId == companyId && found.giveAdminPrivledges == true)
            .ToListAsync();

            return listOfAllStaff;
        }

        public async Task<IList<v2_UserStripe>> getNonAdmins(int companyId)
        {
            IList<v2_UserStripe> listOfAllStaff = await _context.Set<v2_UserStripe>()
            .Where(found => found.companyId == companyId && found.giveAdminPrivledges == false)
            .ToListAsync();

            return listOfAllStaff;
        }
    }
}