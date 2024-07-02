using Autofac;
using PI.Application.Service.Notification;
using PI.Application.Service.Notification.NotificationBuilders;
using PI.Domain.Infrastructure.Hub;
using PI.Domain.Models;
using System.Reflection;

namespace PI.Application.Configuration
{
    public static class DependenncyInjection
    {
        public static void RegisterApplicationServices(this ContainerBuilder builder)
        {
            // Register for services
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();



            builder.RegisterType<NotificationBuilder>().As<INotificationBuilder>().InstancePerLifetimeScope();
            builder.RegisterType<StockCheckNotificationBuilder>().As<INotificationDecorator<StockCheck>>().InstancePerLifetimeScope();

        }
    }
}
