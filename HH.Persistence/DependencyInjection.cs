using Autofac;
using HH.Domain.Common;
using HH.Domain.Models;
using HH.Domain.Repositories;
using HH.Domain.Repositories.Common;
using HH.Persistence.DbContexts;
using HH.Persistence.Repositories.Common;
using Npgsql;
using System.Data;

namespace HH.Persistence;
public static class DependencyInjection
{
    public static void RegisterPersitenceServices(this ContainerBuilder builder)
    {

        //already registered in HH.Api IServiceCollection, sql manual connection
        builder.Register(c => new NpgsqlConnection(AppConfig.ConnectionStrings.DefaultConnection))
              .As<IDbConnection>()
              .InstancePerLifetimeScope();

        //builder.RegisterType<AuditInterceptor>().InstancePerLifetimeScope();

        // connect db auto through dbcontext ef
        builder.RegisterType<HhDatabaseContext>().As<DbContext>() .InstancePerLifetimeScope();

        //.EnableClassInterceptors()
        //.InterceptedBy(typeof(AuditInterceptor));

        builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
    }
}
