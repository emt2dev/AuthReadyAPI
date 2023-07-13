using AuthReadyAPI.DataLayer.Models;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface IV2_User : IV2_GenericRepository<v2_UserStripe>
    {
        public Task<IList<v2_UserStripe>> getAllStaff(int companyId);
    }
}