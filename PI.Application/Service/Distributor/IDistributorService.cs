using PI.Domain.Common;
using PI.Domain.Common.PagedLists;
using PI.Domain.Dto.Distributor;

namespace PI.Application.Service
{
    public interface IDistributorService
    {
        Task<ApiResponse<string>> Create(CreateDistributorRequest request);
        Task<ApiResponse<IPagedList<DistributorResponse>>> Search(string? keySearch,
            PagingQuery? pagingQuery,
            string? orderByStr);
    }
}