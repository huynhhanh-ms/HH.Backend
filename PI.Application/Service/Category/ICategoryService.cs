using PI.Domain.Common;
using PI.Domain.Dto.Category;

namespace PI.Application.Service
{
    public interface ICategoryService
    {
        Task<ApiResponse<IEnumerable<CategoryResponse>>> GetAll();
    }
}