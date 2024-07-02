using PI.Domain.Common.PagedLists;
using System.Net;

namespace PI.Domain.Common
{
    public class ApiResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public string? Message { get; set; } = null;
        public T? Data { get; set; } = default;
    }

    public class PagingApiResponse<T> where T : class
    {
        public HttpStatusCode StatusCode { get; set; }
        public string? Message { get; set; } = null;
        public PagingResponse<T>? Data { get; set; } = default;
    }
}
