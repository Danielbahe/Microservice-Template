using Autofac;
using EventService.Service;
using EventService.Service.Interfaces;

namespace EventService.Config
{
    public class AutofacBuilder
    {
        public static IContainer Container;
        public IContainer Initialize()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Service.EventService>().As<IEventService>();
            builder.RegisterType<EventServiceDto>().As<IEventServiceDto>();
            builder.RegisterType<EventServiceRepository>().As<IEventServiceRepository>();

            Container = builder.Build();

            return Container;
        }
    }
}