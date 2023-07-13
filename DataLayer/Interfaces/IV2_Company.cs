using AuthReadyAPI.DataLayer.Models;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface IV2_Company : IV2_GenericRepository<v2_Company>
    {
        public Task<string> giveAdminPrivledges(string staffEmailAddress, int companyId);
        public Task<string> removeAdminPrivledges(string staffEmailAddress, int companyId);
    }
}
