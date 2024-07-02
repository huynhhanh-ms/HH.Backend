using PI.Domain.Infrastructure.Auth;
using PI.Persitence.Interceptors;

namespace PI.Persitence.DbContexts;

public partial class PharmacyInventoryContext : DbContext
{
    private readonly AuditInterceptor _auditInterceptor;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      => optionsBuilder
        .AddInterceptors(_auditInterceptor)
        .UseLazyLoadingProxies()
        .UseMySql(
          AppConfig.ConnectionStrings.DefaultConnection,
          Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.36-mysql"));
        //.LogTo(Console.WriteLine);

    public PharmacyInventoryContext(AuditInterceptor auditInterceptor)
    {
        _auditInterceptor = auditInterceptor;
    }
}
