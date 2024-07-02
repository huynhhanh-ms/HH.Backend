using Autofac;
using PI.Domain.BackgroundQueue;
using PI.Domain.Enums;
using PI.Domain.Infrastructure.Api;
using PI.Domain.Infrastructure.Auth;
using PI.Domain.Infrastructure.Caching;
using PI.Domain.Infrastructure.Discord;
using PI.Domain.Infrastructure.File;
using PI.Domain.Infrastructure.Hub;
using PI.Infrastructure.Api;
using PI.Infrastructure.Auth;
using PI.Infrastructure.BackgroundQueue;
using PI.Infrastructure.Caching;
using PI.Infrastructure.Discord;
using PI.Infrastructure.Firebase;
using PI.Infrastructure.Hubs.Notification;

namespace PI.Infrastructure.Configuration
{
    public static class DependencyInjection
    {
        public static void RegisterInfrastructureServices(this ContainerBuilder builder)
        {
            builder.RegisterType<FirebaseFileService>().As<IFileService>()
                .Keyed<IFileService>(FileServiceProvider.Firebase)
                .SingleInstance();
            builder.RegisterType<CacheService>().As<ICacheService>().InstancePerDependency();
            builder.RegisterType<PasswordHasher>().As<IPasswordHasher>().InstancePerLifetimeScope();
            builder.RegisterType<CurrentAccount>().As<ICurrentAccount>().InstancePerLifetimeScope();
            builder.RegisterType<ApiHelper>().As<IApiHelper>().InstancePerLifetimeScope();
            builder.RegisterType<JWTTokenService>().As<ITokenService>().InstancePerLifetimeScope();
            builder.RegisterType<NotificationSender>().As<INotificationSender>().InstancePerLifetimeScope();

            builder.RegisterType<BackgroundTaskQueue>().As<IBackgroundTaskQueue>().SingleInstance();
            builder.RegisterType<DiscordService>().As<IDiscordService>().SingleInstance();
        }
    }
}
