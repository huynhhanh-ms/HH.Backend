using PI.Domain.Dto.Category;
using PI.Domain.Models;
using PI.Domain.Repositories.Common;

namespace PI.Domain.Repositories
{
    public interface ICategorySettingRepository : IGenericRepository<CategorySetting>
    {
        Task<IEnumerable<CategoryLimitResponse>> FindAll();
    }
}