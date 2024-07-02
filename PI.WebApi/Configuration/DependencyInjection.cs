using Autofac;
using Microsoft.EntityFrameworkCore;
using PI.Application.Configuration;
using PI.Domain.Configuration;
using PI.Infrastructure.Configuration;
using PI.WebApi.Helpers;

namespace PI.WebApi.Configuration
{
    public static class DependencyInjection
    {
        // Register services for Microsoft DI
        public static void AddServices(this IServiceCollection services)
        {
            
            services.AddDbContext();
	    services.AddCorsPolicy();
            // services.AddInfrastructureServices();
            //services.AddBussinessServices();
        }
        public static void AddDbContext(this IServiceCollection services)
        {
            services.AddDbContext<DbContext>(options =>
            {
                options.UseMySql(connectionString: AppConfig.ConnectionStrings.DefaultConnection, 
                                serverVersion: ServerVersion.AutoDetect(AppConfig.ConnectionStrings.DefaultConnection));
            }, contextLifetime: ServiceLifetime.Transient);

            //services.AddScoped<DbContext, AutoAidLtdContext>();
        }
        
        //add cache
        //public static void AddCache(this IServiceCollection services)
        //{
        //    services.AddStackExchangeRedisCache(options =>
        //    {
        //        options.Configuration = AppConfig.RedisConfig.DefaultConnection;
        //        options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions()
        //        {
        //            AbortOnConnectFail = true,
        //            EndPoints = { AppConfig.RedisConfig.DefaultConnection }
        //        };
        //    });
            
        //}

        // Register services for autofac
        public static void RegisterServices(this ContainerBuilder builder)
        {
            builder.RegisterInfrastructureServices();

            builder.RegisterPersitenceServices();

            builder.RegisterApplicationServices();

            builder.RegisterMiddleware();

            builder.RegisterMapsterMappingTypes();
        }
        
        public static void RegisterMiddleware(this ContainerBuilder builder)
        {
            builder.RegisterType<ExceptionMiddleware>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<CurrentAccountMiddleware>().AsSelf().InstancePerLifetimeScope();
        }
    }
}
