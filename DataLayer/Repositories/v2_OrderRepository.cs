using AuthReadyAPI.DataLayer.DTOs.Pagination;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthReadyAPI.DataLayer.Repositories
{
    public class v2_OrderRepository : v2_GenericRepository<v2_Order>, IV2_Order
    {
        private readonly AuthDbContext _context;
        private readonly String readyForDelivery = "ready for delivery";
        public v2_OrderRepository(AuthDbContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<IList<v2_Order>> getAllCustomerOrders(int companyId, string customerId)
        {
            IList<v2_Order> ordersList = await _context.Set<v2_Order>()
            .Include(found => found.cart)
            .Include(x => x.cart.Items)
            .Where(found => found.cart.companyId == companyId.ToString() && found.cart.customerId == customerId)
            .ToListAsync();

            return ordersList;
        }

        public async Task<IList<v2_Order>> getActiveCustomerOrders(int companyId, string customerId)
        {
            IList<v2_Order> ordersList = await _context.Set<v2_Order>()
            .Where(found => found.cart.companyId == companyId.ToString() && found.cart.customerId == customerId && found.orderCompleted == false)
            .ToListAsync();

            return ordersList;
        }

        public async Task<IList<v2_Order>> getCompletedCustomerOrders(int companyId, string customerId)
        {
            IList<v2_Order> ordersList = await _context.Set<v2_Order>()
            .Where(found => found.cart.companyId == companyId.ToString() && found.cart.customerId == customerId && found.orderCompleted == true)
            .ToListAsync();

            return ordersList;
        }

        public async Task<IList<v2_Order>> getAllCompanyOrders(int companyId)
        {
            IList<v2_Order> ordersList = await _context.Set<v2_Order>()
            .Where(found => found.cart.companyId == companyId.ToString())
            .ToListAsync();

            return ordersList;
        }

        public async Task<IList<v2_Order>> getActiveCompanyOrders(int companyId)
        {
            IList<v2_Order> ordersList = await _context.Set<v2_Order>()
            .Where(found => found.cart.companyId == companyId.ToString() && found.orderCompleted == false)
            .ToListAsync();

            return ordersList;
        }

        public async Task<IList<v2_Order>> getCompletedCompanyOrders(int companyId)
        {
            IList<v2_Order> ordersList = await _context.Set<v2_Order>()
            .Where(found => found.cart.companyId == companyId.ToString() && found.orderCompleted == true)
            .ToListAsync();

            return ordersList;
        }

        public async Task<IList<v2_Order>> getReadyDeliveryOrders(int companyId)
        {
            IList<v2_Order> ordersList = await _context.Set<v2_Order>()
            .Include(bag => bag.cart)
            .Where(found => found.cart.companyId == companyId.ToString() && found.orderCompleted == false && found.delivery==true && found.status == readyForDelivery)
            .ToListAsync();

            return ordersList;
        }
    }
}