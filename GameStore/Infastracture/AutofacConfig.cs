using Autofac;
using Autofac.Integration.Mvc;
using GameStore.BAL.Infastracture;
using GameStore.BAL.Interfaces;
using GameStore.BAL.Service;
using log4net;
using System.Reflection;
using System.Web.Mvc;

namespace GameStore.Infastracture
{
    public class AutofacConfig
    {
        public static IContainer Setup()
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var builder = new ContainerBuilder();

            RegisterDependencies(currentAssembly, builder);

            var container = builder.Build();
            var resolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(resolver);

            return container;
        }

        public static void RegisterDependencies(Assembly currentAssembly, ContainerBuilder builder)
        {
            builder.RegisterInstance(LogManager.GetLogger("LOGGER"));
            builder.RegisterInstance(MapperConfigUi.GetMapper());
            
            // Register your MVC controllers.
            builder.RegisterControllers(currentAssembly);

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            builder.RegisterModule(new BllModule("DefaultConnection"));

            builder.RegisterType<GameService>().As<IGameService>().InstancePerLifetimeScope();
            builder.RegisterType<CommentService>().As<ICommentService>().InstancePerLifetimeScope();
        }

    }
}