using AuthReadyAPI.DataLayer.DTOs.Pagination;
using AuthReadyAPI.DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthReadyAPI.DataLayer.Repositories
{
    public class v2_GenericRepository<T> : IV2_GenericRepository<T> where T : class
    {
        private readonly AuthDbContext _context;

        public v2_GenericRepository(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            _ = await _context.AddAsync(entity);
            _ = await _context.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var EntitySearchingFor = await GetAsyncById(id);
            _ = _context.Set<T>().Remove(EntitySearchingFor);

            _ = await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync<TResult>()
        {
            return await _context.Set<T>()
                .ToListAsync();
        }

        public async Task<PagedResult<T>> GetAllAsync<TResult>(QueryParameters QP)
        {
            var recordCount = await _context.Set<T>().CountAsync();
            var records = await _context.Set<T>()
                .Skip(QP.NextPageNumber)
                .Take(QP.PageSize)
                .ToListAsync();

            return new PagedResult<T>
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

        Task<List<TResult>> IV2_GenericRepository<T>.GetAllAsync<TResult>()
        {
            throw new NotImplementedException();
        }

        Task<PagedResult<TResult>> IV2_GenericRepository<T>.GetAllAsync<TResult>(QueryParameters queryParameters)
        {
            throw new NotImplementedException();
        }
    }
}
