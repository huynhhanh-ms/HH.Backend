using PI.Domain.Dto.Category;
using PI.Domain.Models;
using PI.Domain.Repositories;

namespace PI.Persitence.Repository
{
    public class CategorySettingRepository : GenericRepository<CategorySetting>, ICategorySettingRepository
    {
        public CategorySettingRepository(DbContext context) : base(context)
        {
        }

        public override Task<IPagedList<CategorySetting>> SearchAsync(string keySearch, PagingQuery pagingQuery, string orderBy)
        {
            throw new NotImplementedException();
        }

        public override Task<IPagedList<TResult>> SearchAsync<TResult>(string keySearch, PagingQuery pagingQuery, string orderBy)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CategoryLimitResponse>> FindAll()
        {
            return await _dbSet.AsNoTracking()
                .SelectWithField<CategorySetting, CategoryLimitResponse>()
                .ToListAsync();
        }
    }
}