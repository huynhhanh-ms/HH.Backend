namespace PI.Domain.Infrastructure.Api
{
    public interface IApiHelper
    {
        Task<T> GetAsync<T>(string url, string token = null);
        Task<T> PostAsync<T>(string url, object data, string token = null);
    }
}