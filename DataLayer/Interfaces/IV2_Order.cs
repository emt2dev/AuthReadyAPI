using AuthReadyAPI.DataLayer.Models;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface IV2_Order : IV2_GenericRepository<v2_Order>
    {
        public Task<IList<v2_Order>> getAllCustomerOrders(int companyId, string customerId);
        public Task<IList<v2_Order>> getActiveCustomerOrders(int companyId, string customerId);
        public Task<IList<v2_Order>> getCompletedCustomerOrders(int companyId, string customerId);
        public Task<IList<v2_Order>> getAllCompanyOrders(int companyId);
        public Task<IList<v2_Order>> getActiveCompanyOrders(int companyId);
        public Task<IList<v2_Order>> getCompletedCompanyOrders(int companyId);
        public Task<IList<v2_Order>> getReadyDeliveryOrders(int companyId);
    }
}