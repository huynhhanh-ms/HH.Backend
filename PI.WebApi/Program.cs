using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PI.Domain.Common;
using PI.Infrastructure.BackgroundQueue;
using PI.Infrastructure.Common;
using PI.Infrastructure.Hubs.Notification;
using PI.WebApi.Configuration;
using PI.WebApi.Configuration.OpenApi;
using PI.WebApi.Helpers;
using Serilog;


namespace PI.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog(StaticLogger.Configure);

            // Register appsettings.json global variables
            builder.Configuration.SettingsBinding();

            builder.Services.AddMvc();

            // Add services to the container.
            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConfig.JwtSetting.IssuerSigningKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    };
                    // SignalR
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = async context =>
                        {
                            var accessToken = context.Request.Query["access_token"];
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs/notification"))
                            {
                                context.Token = accessToken;
                            }

                            await Task.CompletedTask;
                        }
                    };
                });

            builder.Services.AddStackExchangeRedisCache(
                options => { options.Configuration = AppConfig.RedisConfig.DefaultConnection; }
            );
            builder.Services.AddHttpClient();

            builder.Services.AddControllers();

            // Register services to DI 

            builder.Services.AddServices();
            builder.Services.AddSignalR();
            builder.ConfigureAutofacContainer();

            // Register FluentValidation
            builder.Services.AddFluentValidation();

            //Config endpoints router
            builder.Services.Configure<RouteOptions>(options => { options.LowercaseUrls = true; });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddSwagger();

            builder.Services.AddHostedService<BackgroundQueueService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseSwaggers();

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<CurrentAccountMiddleware>();
            app.UseCorsPolicy();

            app.MapHub<NotificationHub>("/hubs/notification");

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}