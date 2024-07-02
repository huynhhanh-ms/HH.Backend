using Microsoft.EntityFrameworkCore;
using PI.Domain.Common.PagedLists;
using PI.Domain.Models;
using PI.Persitence.Repository.Common;
using PI.Persitence.Repository.Helper;

namespace PI.Persitence.Repository
{
    public class UnitRepository : GenericRepository<Unit>
    {
        public UnitRepository(DbContext context) : base(context)
        {
        }

        public override Task<IPagedList<Unit>> SearchAsync(string keySearch, PagingQuery pagingQuery, string orderBy)
        {
            return _dbSet.AsNoTracking()
                .WhereWithExist(p => string.IsNullOrEmpty(keySearch) ||
                                      p.Name.Contains(keySearch))
                .WithOrderByString(orderBy)
                .ToPagedListAsync(pagingQuery);
        }

        public override Task<IPagedList<TResult>> SearchAsync<TResult>(string keySearch, PagingQuery pagingQuery, string orderBy)
        {
            return _dbSet.AsNoTracking()
                .WhereWithExist(p => string.IsNullOrEmpty(keySearch) ||
                                      p.Name.Contains(keySearch))
                .WithOrderByString(orderBy)
                .SelectWithField<Unit, TResult>()
                .ToPagedListAsync(pagingQuery);
        }
    }
}