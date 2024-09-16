using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using HH.Domain.Common;
using HH.Domain.Common.Entity;

namespace HH.Domain.Repositories.Common
{
    public interface IGenericRepository<TEntity> : IRepository where TEntity : class, IEntityBase
    {
        #region Query
        Task<TEntity?> FindAsync(int entityId);
        Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindListAsync(Expression<Func<TEntity, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TResult>> GetAllAsync<TResult>()
            where TResult : class;
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null
            );
        Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, bool>>? predicate = null)
            where TResult : class;
        Task<IDictionary<TKey, TEntity>> GetAllAsync<TKey>(Func<TEntity, TKey> keySelector, Expression<Func<TEntity, bool>>? predicate = null)
           where TKey : notnull;

        Task<IPagedList<TEntity>> SearchAsync(string? keySearch, PagingQuery pagingQuery, string? orderBy);
        Task<IPagedList<TResult>> SearchAsync<TResult>(string? keySearch, PagingQuery pagingQuery, string? orderBy)
            where TResult : class;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSearchRequest">Type of Search Request</typeparam>
        /// <typeparam name="TResult">Type of Response</typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IPagedList<TResult>> SearchAsync<TSearchRequest, TResult>(TSearchRequest request)
            where TSearchRequest : SearchBaseRequest
            where TResult : class;

        Task<bool> IsExist(params int[] ids);
        Task<bool> AreAllExistedAsync(IEnumerable<object> keys);
        #endregion Query

        #region Command 
        Task CreateAsync(params TEntity[] entities);
        Task UpdateAsync(params TEntity[] entities);
        Task DeleteAsync(params int[] ids);
        #endregion Command 
    }
}
