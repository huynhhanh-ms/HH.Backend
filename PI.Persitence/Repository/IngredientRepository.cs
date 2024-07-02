using Microsoft.EntityFrameworkCore;
using PI.Domain.Common.PagedLists;
using PI.Domain.Models;
using PI.Domain.Repositories;
using PI.Persitence.Repository.Common;

namespace PI.Persitence.Repository
{
    public class IngredientRepository : GenericRepository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(DbContext context) : base(context)
        {
        }

        public override Task<IPagedList<Ingredient>> SearchAsync(string keySearch, PagingQuery pagingQuery, string orderBy)
        {
            throw new NotImplementedException();
        }

        public override Task<IPagedList<TResult>> SearchAsync<TResult>(string keySearch, PagingQuery pagingQuery, string orderBy)
        {
            throw new NotImplementedException();
        }

        public Task<Ingredient?> FindByName(string name)
        {
            return _dbSet.AsNoTracking()
                .Include(x => x.Medicines)
                .FirstOrDefaultAsync(p => p.FullName == name);

        }
    }
}