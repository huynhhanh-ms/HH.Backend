using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using PI.Domain.Enums;
using PI.Domain.Infrastructure.Caching;

namespace PI.Infrastructure.Caching
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;

        public CacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }


        public async Task<T?> GetAsync<T>(string key) =>
            await GetAsync(key) is { } data
                ? JsonConvert.DeserializeObject<T>(Encoding.Default.GetString(data))
                : default;

        private async Task<byte[]?> GetAsync(string key)
        {
            try
            {
                return await _distributedCache.GetAsync(key);
            }
            catch
            {
                return null;
            }
        }


        public async Task RefreshAsync(string key)
        {
            try
            {
                await _distributedCache.RefreshAsync(key);
            }
            catch
            {
            }
        }

        public async Task RemoveAsync(string key)
        {
            try
            {
                await _distributedCache.RemoveAsync(key);
            }
            catch
            {
                
            }
        }

        public string GetKeyName(KeyType keyType, string key)
        {
            var result = string.Empty;
            if (keyType == KeyType.RefreshToken)
            {
                result = $"refresh_token::{key}";
            }

            return result;
        }


        public Task SetAsync<T>(string key, T value, TimeSpan? slidingExpiration = null)
        {
            return SetAsync(key, Encoding.Default.GetBytes(JsonConvert.SerializeObject(value)), slidingExpiration);
        }
        private async Task SetAsync(string key, byte[] value, TimeSpan? slidingExpiration = null)
        {
            try
            {
                await _distributedCache.SetAsync(key, value, GetOptions(slidingExpiration));
            }
            catch
            {
            }
        }
        private static DistributedCacheEntryOptions GetOptions(TimeSpan? slidingExpiration)
        {
            var options = new DistributedCacheEntryOptions();
            if (slidingExpiration.HasValue)
            {
                options.SetSlidingExpiration(slidingExpiration.Value);
            }
            else
            {
                // TODO: add to appsettings?
                options.SetSlidingExpiration(TimeSpan.FromMinutes(10)); // Default expiration time of 10 minutes.
            }

            return options;
        }
    }
}