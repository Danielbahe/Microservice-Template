using Autofac;

namespace PersonSevice.Config
{
    public class AutofacBuilder
    {
        public static IContainer Container;
        public IContainer Initialize()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<PersonService>().As<IPersonService>();
            builder.RegisterType<PersonServiceDto>().As<IPersonServiceDto>();
            
            Container = builder.Build();

            return Container;
        }
        
    }
}