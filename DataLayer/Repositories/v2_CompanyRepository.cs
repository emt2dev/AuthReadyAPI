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
        private readonly v2_UserStripe _user;
        private readonly UserManager<v2_UserStripe> _UM;
        public v2_CompanyRepository(AuthDbContext context, UserManager<v2_UserStripe> UM) : base(context)
        {
            this._context = context;
            this._UM = UM;
        }

        public async Task<string> giveAdminPrivledges(string staffEmailAddress, int companyId)
        {
            v2_UserStripe staffFound = await _UM.FindByEmailAsync(staffEmailAddress);

            staffFound.giveAdminPrivledges = true;

            await _UM.UpdateAsync(staffFound);

            v2_Company companyFound = await GetAsyncById(companyId);
            if (companyFound.administratorOne == null) companyFound.administratorOne = staffFound;
            else companyFound.administratorTwo = staffFound;

            await UpdateAsync(companyFound);

            return "updated";
        }

        public async Task<string> removeAdminPrivledges(string staffEmailAddress, int companyId)
        {
            v2_UserStripe staffFound = await _UM.FindByEmailAsync(staffEmailAddress);

            staffFound.giveAdminPrivledges = false;

            await _UM.UpdateAsync(staffFound);

            v2_Company companyFound = await GetAsyncById(companyId);
            if (companyFound.administratorOne == staffFound) companyFound.administratorOne = null;
            else if (companyFound.administratorTwo == staffFound) companyFound.administratorTwo = null;

            await UpdateAsync(companyFound);

            return "updated";
        }
    }
}