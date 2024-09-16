using HH.Domain;
using System.Runtime.InteropServices;

namespace HH.Api.Configuration
{
    public static class AppSettingsRegister
    {
        public static void SettingsBinding(this IConfiguration configuration)
        {
            do
            {
                AppConfig.ConnectionStrings = new ConnectionStrings();
                AppConfig.FirebaseConfig = new FirebaseConfig();
                AppConfig.JwtSetting = new JwtSetting();
                AppConfig.CorsConfig = new CorsConfig();
                AppConfig.RedisConfig = new RedisConfig();
                AppConfig.SwaggerConfig = new SwaggerConfig();
                AppConfig.SentryConfig = new SentryConfig();
                AppConfig.DiscordConfig = new DiscordConfig();
            } while (AppConfig.ConnectionStrings == null);

            configuration.Bind("ConnectionStrings", AppConfig.ConnectionStrings);
            configuration.Bind("FirebaseConfig", AppConfig.FirebaseConfig);
            configuration.Bind("JwtSetting", AppConfig.JwtSetting);
            configuration.Bind("CorsConfig", AppConfig.CorsConfig);
            configuration.Bind("RedisConfig", AppConfig.RedisConfig);
            configuration.Bind("SwaggerConfig", AppConfig.SwaggerConfig);
            configuration.Bind("Sentry", AppConfig.SentryConfig);
            configuration.Bind("Discord", AppConfig.DiscordConfig);

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                AppConfig.IsDevelopmentEnvironment = false;
            else
                AppConfig.IsDevelopmentEnvironment = true;
        }
    }
}
