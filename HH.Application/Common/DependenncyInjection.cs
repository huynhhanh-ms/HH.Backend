using Autofac;
using HH.Application.Services;

//using PI.Application.Service.Notification;
//using PI.Application.Service.Notification.NotificationBuilders;
//using PI.Domain.Infrastructure.Hub;
using HH.Domain.Common;
using HH.Domain.Repositories.Common;
using HH.Persistence.Repositories.Common;
using System.Reflection;

namespace HH.Application.Common
{
    public static class DependenncyInjection
    {
        public static void RegisterApplicationServices(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(CrudRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            //builder.RegisterType<AccountService>().As<IAccountService>().InstancePerLifetimeScope();
            //builder.RegisterType<StockCheckNotificationBuilder>().As<INotificationDecorator<StockCheck>>().InstancePerLifetimeScope();
            //builder.RegisterType<ShipmentNotificationBuilder>().As<INotificationDecorator<Shipment>>().InstancePerLifetimeScope();
            //builder.RegisterType<ExportRequestNotificationBuilder>().As<INotificationDecorator<ExportRequest>>().InstancePerLifetimeScope();
            //builder.RegisterType<ImportRequestNotificationBuilder>().As<INotificationDecorator<ImportRequest>>().InstancePerLifetimeScope();

        }
    }
}
