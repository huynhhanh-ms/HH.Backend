using PI.Domain.Common;

namespace PI.WebApi.Configuration
{
    public static class CorsConfigurations
    {
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