using NuGet.Packaging.Licenses;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface ICacheService
    {
        // Uses generics
        public Task<T> GetData<T> (string key);
        public Task<bool> SetData<T>(string key, T value, DateTimeOffset expirationTime);
        public Task<object> RemoveData(string key);
        public Task<bool> AddDemoList<T>(List<T> DataSet);
        public Task<List<T>> GetDemoList<T>();
    }
}
