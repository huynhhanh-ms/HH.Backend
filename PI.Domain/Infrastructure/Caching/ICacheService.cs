using PI.Domain.Enums;

namespace PI.Domain.Infrastructure.Caching
{
    public interface ICacheService
    {

        Task<T?> GetAsync<T>(string key);

        Task RefreshAsync(string key);

        Task RemoveAsync(string key);

        Task SetAsync<T>(string key, T value, TimeSpan? slidingExpiration = null);
        string GetKeyName(KeyType keyType, string key);

    }
}