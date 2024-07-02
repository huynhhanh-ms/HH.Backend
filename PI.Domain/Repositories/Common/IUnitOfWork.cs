using PI.Domain.Common.Entity;

namespace PI.Domain.Repositories.Common
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Resolve a repository for a specific entity type.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IGenericRepository<TEntity> Resolve<TEntity>()
            where TEntity : class, IEntity;

        /// <summary>
        /// Resolve a repository for a specific repository inteface.
        /// </summary>
        /// <typeparam name="IEntityRepository"></typeparam>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public IEntityRepository Resolve<IEntityRepository>(Type? entityType = null)
            where IEntityRepository : IRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TEntityRepository"></typeparam>
        /// <returns></returns>
        //TEntityRepository Resolve<TEntity, TEntityRepository>()
        //    where TEntity : class, IEntity
        //    where TEntityRepository : IGenericRepository<TEntity>;
    }
}
