using Microsoft.EntityFrameworkCore;
using PI.Domain.Common.PagedLists;
using PI.Domain.Models;
using PI.Domain.Repositories;
using PI.Persitence.Repository.Common;

namespace PI.Persitence.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DbContext context) : base(context)
        {
        }

        public override Task<IPagedList<Category>> SearchAsync(string keySearch, PagingQuery pagingQuery, string orderBy)
        {
            throw new NotImplementedException();
        }

        public override Task<IPagedList<TResult>> SearchAsync<TResult>(string keySearch, PagingQuery pagingQuery, string orderBy)
        {
            throw new NotImplementedException();
        }

        public Task<Category?> FindById(int id)
        {
            return _dbSet.AsNoTracking()
                .Include(p => p.Parent)
                .FirstOrDefaultAsync(p => p.CategoryId == id);
        }
    }
}