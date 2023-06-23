using AuthReadyAPI.DataLayer.DTOs.Pagination;
using AuthReadyAPI.DataLayer.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AuthReadyAPI.DataLayer.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AuthDbContext _context;
        private readonly IMapper _mapper;

        public GenericRepository(AuthDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var EntitySearchingFor = await GetAsyncById(id);
            _context.Set<T>().Remove(EntitySearchingFor);

            await _context.SaveChangesAsync();
        }

        public async Task<List<TResult>> GetAllAsync<TResult>()
        {
            return await _context.Set<T>()
                .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<PagedResult<TResult>> GetAllAsync<TResult>(QueryParameters QP)
        {
            var recordCount = await _context.Set<T>().CountAsync();
            var records = await _context.Set<T>()
                .Skip(QP.NextPageNumber)
                .Take(QP.PageSize)
                .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new PagedResult<TResult>
            {
                Records = records,
                PageNumber = QP.NextPageNumber,
                TotalCount = recordCount
            };
        }

        public async Task<T> GetAsyncById(int? id)
        {
            if (id == null) return null;

            return await _context.Set<T>()
                .FindAsync(id);
        }

        public async Task<bool> RowExists(int id)
        {
            var EntitySearchingFor = await GetAsyncById(id);
            return EntitySearchingFor != null;
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
