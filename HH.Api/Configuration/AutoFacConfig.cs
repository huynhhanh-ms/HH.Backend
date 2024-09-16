using Autofac;
using Autofac.Extensions.DependencyInjection;
using HH.Domain.Common;

namespace HH.Api.Configuration
{
    public static class AutoFacConfigurations
    {
        public static void ConfigureAutofacContainer(this WebApplicationBuilder builder)
        {
            //Config Autofac
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            //Config Autofac Container
            builder.Host.ConfigureContainer<ContainerBuilder>(container =>
            {
                container.RegisterModule(new AutofacModule());
            });
        }
    }

    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterServices();

            AppContainter.Load(containerBuilder);

            //Register Services
            builder.RegisterServices();

            //Register background queue
            //builder.RegisterType<BackgroundQueueService>()
            //.As<IHostedService>()
            //.SingleInstance();

            base.Load(builder);
        }
    }
}
