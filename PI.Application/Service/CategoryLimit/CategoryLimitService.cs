using PI.Domain.Dto.Category;
using PI.Domain.Models;
using PI.Domain.Repositories;
using PI.Domain.Repositories.Common;


namespace PI.Application.Service
{
    public class CategoryLimitService : BaseService, ICategoryLimitService
    {
        public CategoryLimitService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<ApiResponse<string>> Create(CategoryLimitRequest request)
        {
            try
            {
                var category = await _unitOfWork.Resolve<Category>().FindAsync(p => p.CategoryId == request.CategoryId);
                if (category == null)
                {
                    return Failed<string>("Category not found", HttpStatusCode.BadRequest);
                }
                if (category.HasChildren)
                {
                    return Failed<string>("Category is not a valid category", HttpStatusCode.BadRequest);
                }
                //check max and min quantity
                if (request.MaxQuantity < 0 || request.MinQuantity < 0)
                {
                    return Failed<string>("Max and Min quantity must be greater than 0", HttpStatusCode.BadRequest);
                }
                if (request.MaxQuantity < request.MinQuantity)
                {
                    return Failed<string>("Max quantity must be greater than Min quantity", HttpStatusCode.BadRequest);
                }

                var categoryLimit = request.Adapt<CategorySetting>();
                await _unitOfWork.Resolve<CategorySetting>().CreateAsync(categoryLimit);
                await _unitOfWork.SaveChangesAsync();
                return Success<string>("Category limit created successfully");
            }
            catch (Exception ex)
            {
                return Failed<string>(ex.GetExceptionMessage());
            }
        }

        public async Task<ApiResponse<IEnumerable<CategoryLimitResponse>>> GetAll()
        {
            try
            {
                var responses = await _unitOfWork.Resolve<ICategorySettingRepository>().FindAll();
                return Success(responses);
            }
            catch (Exception ex)
            {
                return Failed<IEnumerable<CategoryLimitResponse>>(ex.GetExceptionMessage());
            }
        }
    }
}