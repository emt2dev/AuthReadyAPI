using AuthReadyAPI.DataLayer.DTOs.Company;
using AuthReadyAPI.DataLayer.DTOs.Pagination;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace AuthReadyAPI.DataLayer.Repositories
{
    public class ProductRepository : GenericRepository<ProductDTO>, IProduct
    {
        private readonly AuthDbContext _context;
        private readonly IMapper _mapper;
        public ProductRepository(AuthDbContext context, IMapper mapper) : base(context, mapper)
        {
            this._mapper = mapper;
            this._context = context;
        }

        public async Task<List<ProductDTO>> GetAllAvailableAPIProducts()
        {
            return await _context.Products.Where(x => x.IsAvailableForOrder == true).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<List<ProductDTO>> GetAllAvailableCompanyProducts(int CompanyId)
        {
            return await _context.Products.Where(x => x.CompanyId == CompanyId && x.IsAvailableForOrder == true).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<List<ProductDTO>> GetAPIProducts()
        {
            return await _context.Products.ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<List<ProductDTO>> GetAPIProductsByCategoryId(int CategoryId)
        {
            return await _context.Products.Where(x => x.CategoryId == CategoryId).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<List<ProductDTO>> GetAPIProductsByKeyword(string Keyword)
        {
            if(!_context.Categories.Any(x => x.Name == Keyword)) return new List<ProductDTO>();

            CategoryClass Found = await _context.Categories.Where(x => x.Name == Keyword).FirstOrDefaultAsync();
            _context.ChangeTracker.Clear();

            if (Found is null) return new List<ProductDTO>(); 

            return await _context.Products.Where(x => x.CategoryId == Found.Id).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<List<ProductDTO>> GetCompanyProducts(int CompanyId)
        {
            return await _context.Products.Where(x => x.CompanyId == CompanyId).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<List<ProductDTO>> GetCompanyProductsByCategoryId(int CategoryId, int CompanyId)
        {
            if (!_context.Categories.Any(x => x.Id == CategoryId && x.CompanyId == CompanyId)) return new List<ProductDTO>();

            return await _context.Products.Where(x => x.CategoryId == CategoryId && x.CompanyId == CompanyId).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<List<ProductDTO>> GetCompanyProductsByKeyword(string Keyword, int CompanyId)
        {
            if (!_context.Categories.Any(x => x.Name == Keyword && x.CompanyId == CompanyId)) return new List<ProductDTO>();

            CategoryClass Found = await _context.Categories.Where(x => x.Name == Keyword && x.CompanyId == CompanyId).FirstOrDefaultAsync();
            _context.ChangeTracker.Clear();

            if (Found is null) return new List<ProductDTO>();

            return await _context.Products.Where(x => x.CategoryId == Found.Id && x.CompanyId == CompanyId).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<ProductDTO> GetProduct(int ProductId)
        {
            if (!_context.Products.Any(x => x.Id == ProductId)) return null;
            return await _context.Products.Where(x => x.Id == ProductId).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        }

        public ProductDTO GetProductCartCount()
        {
            return _context.Products.OrderByDescending(p => p.CartCount)
                .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefault();

        }

        public ProductDTO GetProductCartCount(int CompanyId)
        {
            return _context.Products.Where(x => x.CompanyId == CompanyId)
                .OrderByDescending(p => p.CartCount)
                .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefault();
        }

        public ProductDTO GetProductGrossIncome()
        {
            return _context.Products.OrderByDescending(p => p.GrossIncome)
                .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefault();
        }

        public ProductDTO GetProductGrossIncome(int CompanyId)
        {
            return _context.Products.Where(x => x.CompanyId == CompanyId)
                .OrderByDescending(p => p.GrossIncome)
                .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefault();
        }

        public ProductDTO GetProductOrderCount()
        {
            return _context.Products.OrderByDescending(p => p.OrderCount)
                .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefault();
        }

        public ProductDTO GetProductOrderCount(int CompanyId)
        {
            return _context.Products.Where(x => x.CompanyId == CompanyId)
                .OrderByDescending(p => p.OrderCount)
    .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
    .FirstOrDefault();
        }
    }
}
