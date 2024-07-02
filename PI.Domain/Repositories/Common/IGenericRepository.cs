using System.Linq.Expressions;
using PI.Domain.Common.Entity;
using PI.Domain.Common.PagedLists;

namespace PI.Domain.Repositories.Common
{
    public interface IGenericRepository<TEntity> : IRepository where TEntity : class, IEntity
    {
        #region Query
        Task<TEntity?> FindAsync(int entityId);
        Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindListAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TResult>> GetAllAsync<TResult>() where TResult : class;
        Task<IPagedList<TEntity>> SearchAsync(string keySearch, PagingQuery pagingQuery, string orderBy);
        Task<IPagedList<TResult>> SearchAsync<TResult>(string keySearch, PagingQuery pagingQuery, string orderBy)
            where TResult : class;
        Task<bool> IsExist(params int[] ids);
        #endregion Query

        #region Command 
        Task CreateAsync(params TEntity[] entities);
        Task UpdateAsync(params TEntity[] entities);
        Task DeleteAsync(params int[] ids);
        #endregion Command 
    }
}
