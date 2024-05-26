using AuthReadyAPI.DataLayer.Models;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface IDemoRepository
    {
        public Task<bool> CreateNewDemoClasses();
        public Task<List<CacheDemoClass>> ReturnFromDb();
        public Task<bool> ReturnFromDBAsQueryable();
        public Task<bool> ReturnFromDBAsList();
    }
}
