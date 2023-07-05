using AuthReadyAPI.DataLayer.DTOs.Company;
using AuthReadyAPI.DataLayer.DTOs.Pagination;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace AuthReadyAPI.DataLayer.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProduct
    {
        private readonly AuthDbContext _context;
        private readonly IMapper _mapper;
        public ProductRepository(AuthDbContext context, IMapper mapper) : base(context, mapper)
        {
            this._mapper = mapper;
            this._context = context;
        }

        // public async Task<PagedResult<Product>> GET__PRODUCT__ALL(int companyId, QueryParameters QP)
        public async Task<IList<Product>> GET__PRODUCT__ALL(int companyId)
        {
            // var recordCount = await _context.Set<Product>().CountAsync();
            // var records = await _context.Set<Product>()
            //     .Where(found => found.Company == companyId.ToString())
            //     .Skip(QP.NextPageNumber)
            //     .Take(QP.PageSize)
            //     .ToListAsync();

            // return new PagedResult<Product>
            // {
            //     Records = records,
            //     PageNumber = QP.NextPageNumber,
            //     TotalCount = recordCount
            // };

            var matchingProducts = await _context.Set<Product>()
                .Where(found => found.Company == companyId.ToString())
                .ToListAsync();

            return matchingProducts;
        }

        public async Task<PagedResult<Product>> GET__PRODUCT__KEYWORD__ALL(int companyId, QueryParameters QP, string keyword)
        {
            var recordCount = await _context.Set<Product>().CountAsync();
            var records = await _context.Set<Product>()
                .Where(found => found.Company == companyId.ToString() && found.Keyword == keyword)
                .Skip(QP.NextPageNumber)
                .Take(QP.PageSize)
                .ToListAsync();

            return new PagedResult<Product>
            {
                Records = records,
                PageNumber = QP.NextPageNumber,
                TotalCount = recordCount
            };
        }

        public async Task<Product> GET__PRODUCT__ONE(int productId)
        {

            Product productFound = await this.GetAsyncById(productId);

            return productFound;
        }
    }
}
