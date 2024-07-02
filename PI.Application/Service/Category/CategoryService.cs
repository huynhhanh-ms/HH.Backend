using PI.Domain.Dto.Category;
using PI.Domain.Models;
using PI.Domain.Repositories;
using PI.Domain.Repositories.Common;

namespace PI.Application.Service
{
    public class CategoryService : BaseService, ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        //get all categories
        public async Task<ApiResponse<IEnumerable<CategoryResponse>>> GetAll()
        {
            var categories = await _unitOfWork.Resolve<Category>().GetAllAsync();
            var responses = categories.Adapt<IEnumerable<CategoryResponse>>();
            return Success(responses);
        }
    }
}