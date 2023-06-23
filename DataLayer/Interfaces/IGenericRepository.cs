using AuthReadyAPI.DataLayer.DTOs.Pagination;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetAsyncById(int? id); // gets the row that has the id
        Task<List<TResult>> GetAllAsync<TResult>(); // gets "all" of rows of the type
        Task<PagedResult<TResult>> GetAllAsync<TResult>(QueryParameters queryParameters); // Implements pagination
        Task<T> AddAsync(T entity); // adds row
        Task UpdateAsync(T entity); // Updates row, does not return anything
        Task DeleteAsync(int id); // deletes a row
        Task<bool> RowExists(int id); // determines if id belongs to row
    }
}
