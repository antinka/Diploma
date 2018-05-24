using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using GameStore.BLL.Infrastructure;

namespace GameStore.Web.Infrastructure
{
    public class AutofacConfig
    {
        public static void Setup()
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var builder = new ContainerBuilder();

            RegisterDependencies(currentAssembly, builder);

            var container = builder.Build();
            var resolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(resolver);
        }

        public static void RegisterDependencies(Assembly currentAssembly, ContainerBuilder builder)
        {
            builder.RegisterControllers(currentAssembly);

            builder.RegisterFilterProvider();
            builder.RegisterModule(new WebModule());
            builder.RegisterModule(new BllModule("DefaultConnection"));
        }
    }
}