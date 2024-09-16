using Microsoft.EntityFrameworkCore.Storage;
using HH.Domain.Common.Entity;
using HH.Domain.Repositories.Common;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace HH.Persistence.Repositories.Common;

public class UnitOfWork : IUnitOfWork
{
    private bool isTransactionOpening = false;
    private readonly DbContext _dbContext;
    private readonly Dictionary<string, object> _repositoryDictionary;
    private IDbContextTransaction? transaction;

    public UnitOfWork(DbContext dbContext)
    {
        _dbContext = dbContext;
        _repositoryDictionary = new Dictionary<string, object>();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (isTransactionOpening)
            throw new InvalidOperationException("Transaction has already been started.");

        transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        isTransactionOpening = true;
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            if (transaction == null)
                throw new InvalidOperationException("Transaction has not been started.");

            await transaction.CommitAsync(cancellationToken);
            isTransactionOpening = false;
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (transaction == null || isTransactionOpening == false)
            throw new InvalidOperationException("Transaction has not been started.");

        await transaction.RollbackAsync(cancellationToken);
        isTransactionOpening = false;
    }


    public IGenericRepository<TEntity> Resolve<TEntity>()
        where TEntity : class, IEntity
    {
        var repository = GetOrCreateRepository<IGenericRepository<TEntity>>(typeof(TEntity).Name);
        return repository;
    }

    public TEntityRepository Resolve<TEntityRepository>(Type? entityType = null)
        where TEntityRepository : IRepository
    {
        entityType ??= typeof(TEntityRepository).GetInterface("IGenericRepository`1")?.GenericTypeArguments.FirstOrDefault();

        if (entityType?.GetInterface(nameof(IEntityBase)) != null) // entity is derived from IEntity
        {
            return GetOrCreateRepository<TEntityRepository>(entityType.Name);
        }

        throw new ArgumentException("Entity must be derived from IEntity");
    }

    public TEntityRepository Resolve<TEntity, TEntityRepository>()
        where TEntity : class, IEntity
        where TEntityRepository : IGenericRepository<TEntity>
    {
        return GetOrCreateRepository<TEntityRepository>(nameof(TEntity));
    }

    private Repository GetOrCreateRepository<Repository>(string entityName)
            where Repository : IRepository
    {
        var instance = _repositoryDictionary.GetValueOrDefault(entityName);

        if (instance != null && instance is Repository repository)
            return repository;

        instance = CreateInstance<Repository>();

        repository = (Repository)instance;
        _repositoryDictionary.Add(entityName, repository);

        return repository;
    }

    private object CreateInstance<TRepository>()
    {
        var classRepository = GetClassImplementingInterface(typeof(TRepository));

        var instance = Activator.CreateInstance(classRepository, _dbContext);

        ArgumentNullException.ThrowIfNull(instance, $"Cannot create instance of {classRepository.Name}");

        return instance;
    }

    private Type GetClassImplementingInterface(Type interfaceType)
    {
        var genericType = interfaceType.GenericTypeArguments.FirstOrDefault();

        Type? type = null;
        if (genericType != null)
        {
            type = Assembly.GetExecutingAssembly().GetTypes()
                .FirstOrDefault(t => t.IsClass == true && t.IsAbstract == false
                                                       && (t.GetInterface(interfaceType.Name)
                                                           ?.GetGenericArguments()
                                                           ?.Any(a => a.Name == genericType.Name) ?? false));
        }
        else
        {
            type = Assembly.GetExecutingAssembly().GetTypes()
                .FirstOrDefault(t => t.IsClass == true && t.IsAbstract == false
                                                       && t.GetInterface(interfaceType.Name) != null);
        }


        ArgumentNullException.ThrowIfNull(type, $"Cannot find class implementing interface {interfaceType.Name}");
        return type;
    }

    #region Destructor
    private bool isDisposed = false;
    protected virtual void Dispose(bool disposing)
    {
        if (isDisposed)
            return;

        if (disposing)
        {
            _dbContext.Dispose();
        }

        isDisposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~UnitOfWork()
    {
        Dispose(false);
    }
    #endregion Destructor
}

