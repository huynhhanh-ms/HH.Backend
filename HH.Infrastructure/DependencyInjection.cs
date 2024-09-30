using Autofac;

//using HH.Domain.BackgroundQueue;
using HH.Domain.Enums;
//using HH.Domain.Infrastructure.Api;
using HH.Domain.Infrastructure.Auth;

//using HH.Domain.Infrastructure.Caching;
//using HH.Domain.Infrastructure.Discord;
//using HH.Domain.Infrastructure.File;
//using HH.Domain.Infrastructure.Hub;
//using HH.Domain.Infrastructure.Locker;
//using HH.Infrastructure.Api;
using HH.Infrastructure.Auth;
using PI.Infrastructure.Auth;
//using HH.Infrastructure.BackgroundQueue;
//using HH.Infrastructure.Caching;
//using HH.Infrastructure.Discord;
//using HH.Infrastructure.Firebase;
//using HH.Infrastructure.Hubs.Notification;
//using HH.Infrastructure.Hubs.Simulation;
//using HH.Infrastructure.Lockers;

namespace HH.Infrastructure
{
    public static class DependencyInjection
    {
        public static void RegisterInfrastructureServices(this ContainerBuilder builder)
        {
            //builder.RegisterType<FirebaseFileService>().As<IFileService>()
            //    .Keyed<IFileService>(FileServiceProvider.Firebase)
            //    .SingleInstance();

            //builder.RegisterType<CacheService>().As<ICacheService>().InstancePerDependency();
            builder.RegisterType<PasswordHasher>().As<IPasswordHasher>().InstancePerLifetimeScope();
            builder.RegisterType<CurrentAccount>().As<ICurrentAccount>().InstancePerLifetimeScope();
            //builder.RegisterType<ApiHelper>().As<IApiHelper>().InstancePerLifetimeScope();
            builder.RegisterType<JWTTokenService>().As<ITokenService>().InstancePerLifetimeScope();
            //builder.RegisterType<NotificationSender>().As<INotificationSender>().InstancePerLifetimeScope();
            //builder.RegisterType<SimulationSender>().As<ISimulationSender>().InstancePerLifetimeScope();

            //builder.RegisterType<BackgroundTaskQueue>().As<IBackgroundTaskQueue>().SingleInstance();
            //builder.RegisterType<DiscordService>().As<IDiscordService>().SingleInstance();
            //builder.RegisterType<StockLocker>().As<IStockLocker>().SingleInstance();
        }
    }
}
