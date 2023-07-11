using AuthReadyAPI.DataLayer.DTOs.APIUser;
using AuthReadyAPI.DataLayer.DTOs.Pagination;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthReadyAPI.DataLayer.Repositories
{
    public class v2_CompanyRepository : v2_GenericRepository<v2_Company>, IV2_Company
    {
        private readonly AuthDbContext _context;
        private readonly UserManager<v2_Staff> _staffUM;
        public v2_CompanyRepository(AuthDbContext context, UserManager<v2_Staff> staffUM) : base(context)
        {
            this._context = context;
            this._staffUM = staffUM;
        }

        public async Task<string> giveAdminPrivledges(string staffEmailAddress, int companyId)
        {
            v2_Staff staffFound = await _staffUM.FindByEmailAsync(staffEmailAddress);

            staffFound.giveAdminPrivledges = true;

            await _staffUM.UpdateAsync(staffFound);

            v2_Company companyFound = await GetAsyncById(companyId);
            if (companyFound.administratorOne == null) companyFound.administratorOne = staffFound;
            else companyFound.administratorTwo = staffFound;

            await UpdateAsync(companyFound);

            return "updated";
        }

        public async Task<string> removeAdminPrivledges(string staffEmailAddress, int companyId)
        {
            v2_Staff staffFound = await _staffUM.FindByEmailAsync(staffEmailAddress);

            staffFound.giveAdminPrivledges = false;

            await _staffUM.UpdateAsync(staffFound);

            v2_Company companyFound = await GetAsyncById(companyId);
            if (companyFound.administratorOne == staffFound) companyFound.administratorOne = null;
            else if (companyFound.administratorTwo == staffFound) companyFound.administratorTwo = null;

            await UpdateAsync(companyFound);

            return "updated";
        }
    }
}