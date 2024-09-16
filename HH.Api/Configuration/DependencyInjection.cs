using HH.Domain;
using HH.Domain.Models;
using Microsoft.EntityFrameworkCore;

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

    }
}
