using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;
using System.Text.Json;

namespace AuthReadyAPI.DataLayer.Services
{
    public class CacheService : ICacheService
    {
        private IDatabase _cache;

        public CacheService()
        {
            var redis = ConnectionMultiplexer.Connect("localhost:6379"); // the endpoint to connect to redis
            _cache = redis.GetDatabase();
        }

        public async Task<T> GetData<T>(string key)
        {
            var Value = await _cache.StringGetAsync(key);

            if (!string.IsNullOrEmpty(Value)) return JsonSerializer.Deserialize<T>(Value);

            return default;
        }

        public async Task<object> RemoveData(string key)
        {
            var _exists = await _cache.KeyExistsAsync(key);
            if(_exists) return _cache.KeyDeleteAsync(key);

            return false;
        }

        public async Task<bool> SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            // Add to the cache
            var ExpirationTime = expirationTime.DateTime.Subtract(DateTime.Now);
            return await _cache.StringSetAsync(key, JsonSerializer.Serialize(value), ExpirationTime);
        }

        public async Task<bool> AddDemoList<T>(List<T> DataSet)
        {
            var x = JsonSerializer.Serialize(DataSet);
            var ExpirationTime = DateTime.Now.AddSeconds(5) - DateTime.Now;
            return await _cache.StringSetAsync("DemoClassCache", JsonSerializer.Serialize(x), ExpirationTime);
        }

        public async Task<List<T>> GetDemoList<T>()
        {
            var Value = await _cache.StringGetAsync("DemoClassCache");
            
            if (!string.IsNullOrEmpty(Value)) return JsonSerializer.Deserialize<List<T>>(Value);

            return default;
        }
    }
}
