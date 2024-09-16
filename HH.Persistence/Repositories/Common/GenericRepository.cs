using Mapster;
using HH.Domain.Common.Entity;
using HH.Domain.Repositories.Common;
using System.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using HH.Domain.Common;
using HH.Persistence.Repositories.Helper;

namespace HH.Persistence.Repositories.Common
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class, IEntityBase
    {
        //private DapperClient? _dapperDAO = null;

        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DbContext context)
        {
            _dbContext = context;
            _dbSet = context.Set<TEntity>();
        }

        private IDbConnection DbConnection => _dbContext.Database.GetDbConnection();

        //protected DapperClient DapperDAO => _dapperDAO ??= new DapperClient(DbConnection);

        public virtual async Task<TEntity?> FindAsync(int entityId)
        {
            return await _dbSet.FindAsync(entityId);
        }

        public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.WhereWithExist(predicate)
                                .FirstOrDefaultAsync()
                                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> FindListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.WhereWithExist(predicate)
                        .ToListAsync()
                        .ConfigureAwait(false);
        }


        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet
                                .WhereStringWithExist(string.Empty)
                                .ToListAsync()
                                .ConfigureAwait(false);
        }

        public virtual async Task<IEnumerable<TResult>> GetAllAsync<TResult>()
            where TResult : class
        {
            return await _dbSet.WhereStringWithExist(string.Empty)
                                .SelectWithField<TEntity, TResult>()
                                .ToListAsync()
                                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null)
        {
            predicate ??= x => true;
            return await _dbSet
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, bool>>? predicate = null)
            where TResult : class
        {
            predicate ??= x => true;
            return await _dbSet
                .AsNoTracking()
                .Where(predicate)
                .SelectWithField<TEntity, TResult>()
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<IDictionary<TKey, TResult>> GetAllAsync<TKey, TResult>(Func<TResult, TKey> keySelector, Expression<Func<TEntity, bool>>? predicate = null)
            where TKey : notnull
            where TResult : class
        {
            predicate ??= x => true;

            return await _dbSet.AsNoTracking()
                .Where(predicate)
                .SelectWithField<TEntity, TResult>()
                .ToDictionaryAsync(keySelector);
        }

        public async Task<IDictionary<TKey, TEntity>> GetAllAsync<TKey>(Func<TEntity, TKey> keySelector, Expression<Func<TEntity, bool>>? predicate = null)
           where TKey : notnull
        {
            predicate ??= x => true;

            return await _dbSet.AsNoTracking()
                .Where(predicate)
                .ToDictionaryAsync(keySelector);
        }

        public virtual async Task<bool> IsExist(params int[] ids)
        {
            string stringIds = string.Join(",", ids);
            return await _dbSet
                .AsNoTracking()
                .WhereStringWithExist($"e.{EFRepositoryHelpers.GetPrimaryKeyName<TEntity>()} IN ({stringIds})")
                .AnyAsync();
        }

        public virtual async Task<IPagedList<TEntity>> SearchAsync(string? keySearch, PagingQuery pagingQuery, string? orderBy)
        {
            var request = new SearchBaseRequest
            {
                KeySearch = keySearch,
                OrderBy = orderBy,
                PagingQuery = pagingQuery
            };

            var query = _dbSet.AsNoTracking();

            query = ApplyIncludeOperator4Search(query);

            query = query.Where(GetSearchFilterExpression(request));

            query = query.WithOrderByString(request.OrderBy);

            return await query.ToPagedListAsync(request.PagingQuery);
        }

        public virtual async Task<IPagedList<TResult>> SearchAsync<TResult>(string? keySearch, PagingQuery pagingQuery, string? orderBy)
            where TResult : class
        {
            var request = new SearchBaseRequest
            {
                KeySearch = keySearch,
                OrderBy = orderBy,
                PagingQuery = pagingQuery
            };

            var query = _dbSet.AsNoTracking();

            query = ApplyIncludeOperator4Search(query);

            query = query.Where(GetSearchFilterExpression(request));

            query = query.WithOrderByString(request.OrderBy);

            return await query.ToPagedListAsync<TEntity, TResult>(request.PagingQuery);
        }

        public async Task CreateAsync(params TEntity[] entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task UpdateAsync(params TEntity[] entities)
        {
            _dbSet.UpdateRange(entities);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(params int[] ids)
        {
            var condition = $"e.{GetPrimaryKeyName()} IN ({string.Join(",", ids)})";
            var entityDelete = await _dbSet.WhereStringWithExist(condition).ToListAsync();

            foreach (var entity in entityDelete)
            {
                entity.GetType().GetProperty("IsDeleted")?.SetValue(entity, true);
            }

            _dbContext.UpdateRange(entityDelete);
        }

        public virtual async Task<IPagedList<TResult>> SearchAsync<TSearchRequest, TResult>(
            TSearchRequest request)
            where TSearchRequest : SearchBaseRequest
            where TResult : class
        {
            var query = _dbSet.AsNoTracking();

            query = ApplyIncludeOperator4Search(query);

            query = query.Where(GetSearchFilterExpression(request));

            query = query.WithOrderByString(request.OrderBy);

            return await query.ToPagedListAsync<TEntity, TResult>(request.PagingQuery);
        }

        protected virtual Expression<Func<TEntity, bool>> GetSearchFilterExpression<TSearchRequest>(
            TSearchRequest request)
            where TSearchRequest : SearchBaseRequest
        {
            return x => true;
        }

        protected virtual IQueryable<TEntity> ApplyIncludeOperator4Search(IQueryable<TEntity> query)
        {
            return query;
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public async Task<bool> AreAllExistedAsync(IEnumerable<object> keys)
        {
            if (_dbContext == null) throw new ArgumentNullException(nameof(_dbContext));
            if (keys == null || !keys.Any()) return true;

            var entityType = _dbContext.Model.FindEntityType(typeof(TEntity));
            if (entityType == null) throw new InvalidOperationException($"The type '{typeof(TEntity).Name}' is not part of the model for the current context.");

            var primaryKey = entityType.FindPrimaryKey();
            if (primaryKey == null) throw new InvalidOperationException($"The type '{typeof(TEntity).Name}' does not have a primary key defined.");

            var keyProperty = primaryKey.Properties.Single();

            var parameter = Expression.Parameter(typeof(TEntity), "e");
            var propertyAccess = Expression.Property(parameter, keyProperty.Name);
            var keyValues = keys.Cast<object>().ToList();

            var body = keyValues
                .Select(key => Expression.Equal(propertyAccess, Expression.Constant(key)))
                .Aggregate<Expression>((accumulate, equal) => Expression.OrElse(accumulate, equal));

            var lambda = Expression.Lambda<Func<TEntity, bool>>(body, parameter);

            var count = await _dbContext.Set<TEntity>().CountAsync(lambda);

            return count == keyValues.Count;
        }

        private string GetPrimaryKeyName()
        {
            var entityType = _dbContext.Model.FindEntityType(typeof(TEntity));
            if (entityType == null) throw new InvalidOperationException($"The type '{typeof(TEntity).Name}' is not part of the model for the current context.");

            var primaryKey = entityType.FindPrimaryKey();
            if (primaryKey == null) throw new InvalidOperationException($"The type '{typeof(TEntity).Name}' does not have a primary key defined.");

            var keyProperty = primaryKey.Properties.Single();

            return keyProperty.Name;
        }


    }
}
