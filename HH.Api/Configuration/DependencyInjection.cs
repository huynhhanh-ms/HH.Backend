using Autofac;
using HH.Domain.Common;
using HH.Persistence;
using HH.Domain.Models;
using HH.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using HH.Application.Common;
using HH.Persistence.DbContexts;

namespace HH.Api.Configuration
{
    public static class DependencyInjection
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddDbContext();
            //services.AddCorsPolicy();
        }

        //* Add DbContext
        public static void AddDbContext(this IServiceCollection services)
        {
            services.AddDbContext<HhDatabaseContext>(
                options => options.UseNpgsql(connectionString: AppConfig.ConnectionStrings.DefaultConnection),
                contextLifetime: ServiceLifetime.Transient // Setting the context lifetime to Transient
            );
        }

        //* Add Cors Policy
        private const string CorsPolicyName = "CorsPolicy";
        public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            var corsOrigins = AppConfig.CorsConfig.Origins.Split(';');
            return services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicyName, builder =>
                {
                    builder.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .WithOrigins(corsOrigins);
                });
            });
        }
        public static IApplicationBuilder UseCorsPolicy(this IApplicationBuilder app)
        {
            return app.UseCors(CorsPolicyName);
        }

        // Register services for autofac
        public static void RegisterServices(this ContainerBuilder builder)
        {
            builder.RegisterInfrastructureServices();

            builder.RegisterPersitenceServices();

            builder.RegisterApplicationServices();

            //builder.RegisterMiddleware();

            builder.RegisterMapsterMappingTypes();
        }

        //public static void RegisterMiddleware(this ContainerBuilder builder)
        //{
        //    builder.RegisterType<ExceptionMiddleware>().AsSelf().InstancePerLifetimeScope();
        //    builder.RegisterType<CurrentAccountMiddleware>().AsSelf().InstancePerLifetimeScope();
        //}


    }
}
