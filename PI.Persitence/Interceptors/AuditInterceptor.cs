using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PI.Domain.Common.Entity;
using PI.Domain.Infrastructure.Auth;

namespace PI.Persitence.Interceptors
{
    public class AuditInterceptor : SaveChangesInterceptor
    {
        private readonly ICurrentAccount _currentAccount;

        public AuditInterceptor(ICurrentAccount currentAccount)
        {
            _currentAccount = currentAccount;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
        {
            if (eventData.Context is not null)
            {
                UpdateAuditEntities(eventData.Context);
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateAuditEntities(DbContext context)
        {
            DateTime utcNow = DateTime.UtcNow;
            var entities = context.ChangeTracker.Entries().ToList();

            foreach (var entity in entities)
            {
                if (entity.Entity is not IAuditable)
                    continue;

                // when create
                if (entity.State == EntityState.Added)
                {
                    SetCurrentPropertyValue(
                        entity, "UpdatedAt", utcNow);
                    SetCurrentPropertyValue(
                        entity, "CreatedAt", utcNow);
                    SetCurrentPropertyValue(
                        entity, "CreatedBy", _currentAccount.GetAccountId());
                    SetCurrentPropertyValue(
                        entity, "UpdatedBy", _currentAccount.GetAccountId());
                }

                //when update
                if (entity.State == EntityState.Modified)
                {
                    SetCurrentPropertyValue(
                        entity, "UpdatedAt", utcNow);
                    SetCurrentPropertyValue(
                        entity, "UpdatedBy", _currentAccount.GetAccountId());
                }
            }

        }

        private void SetCurrentPropertyValue(EntityEntry entry, string propertyName, object value)
            => entry.Property(propertyName).CurrentValue = value;
    }
}
