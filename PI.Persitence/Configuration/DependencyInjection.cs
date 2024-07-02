using Autofac;
using Autofac.Extras.DynamicProxy;
using MySqlConnector;
using PI.Domain.Repositories.Common;
using PI.Persitence.DbContexts;
using PI.Persitence.Interceptors;
using System.Data;

namespace PI.Infrastructure.Configuration
{
    public static class DependencyInjection
    {
        public static void RegisterPersitenceServices(this ContainerBuilder builder)
        {
            builder.Register(c => new MySqlConnection(AppConfig.ConnectionStrings.DefaultConnection))
                  .As<IDbConnection>()
                  .InstancePerLifetimeScope();

            builder.RegisterType<AuditInterceptor>().InstancePerLifetimeScope();

            builder.RegisterType<PharmacyInventoryContext>().As<DbContext>()
                .InstancePerLifetimeScope();
                //.EnableClassInterceptors()
                //.InterceptedBy(typeof(AuditInterceptor));

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
        }
    }
}
