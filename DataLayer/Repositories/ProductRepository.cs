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

        public async Task<Full__Product> CREATE__PRODUCT(Full__Product DTO)
        {
            Product newProduct = _mapper.Map<Product>(DTO);
            Product result = await this.AddAsync(newProduct);

            Full__Product _DTO = _mapper.Map<Full__Product>(result);

            return _DTO;
        }

        public async Task<PagedResult<Base__Product>> GET__PRODUCT__ALL(int companyId, QueryParameters QP)
        {
            var recordCount = await _context.Set<Product>().CountAsync();
            var records = await _context.Set<Product>()
                .Where(found => found.Company == companyId.ToString())
                .Skip(QP.NextPageNumber)
                .Take(QP.PageSize)
                .ProjectTo<Base__Product>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new PagedResult<Base__Product>
            {
                Records = records,
                PageNumber = QP.NextPageNumber,
                TotalCount = recordCount
            };
        }

        public async Task<Full__Product> GET__PRODUCT__ONE(int productId)
        {

            Product productFound = await this.GetAsyncById(productId);

            Full__Product _DTO = _mapper.Map<Full__Product>(productFound);

            return _DTO;
        }
    }
}
