using PI.Domain.Common;
using PI.Domain.Common.PagedLists;
using PI.Domain.Dto.Category;

namespace PI.Application.Service
{
    public interface ICategoryLimitService
    {
        Task<ApiResponse<string>> Create(CategoryLimitRequest request);
        Task<ApiResponse<IEnumerable<CategoryLimitResponse>>> GetAll();
    }
}