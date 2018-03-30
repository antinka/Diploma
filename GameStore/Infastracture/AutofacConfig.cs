using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using GameStore.BAL.Service;
using GameStore.BAL.Interfaces;
using GameStore.DAL.EF;
using System.Data.Entity;
using GameStore.DAL.Interfaces;
using GameStore.BAL;
using GameStore.BAL.Infastracture;

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
            // Register your MVC controllers.
            builder.RegisterControllers(currentAssembly);

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            builder.RegisterModule(new BLLModule("DefaultConnection")); 
            builder.RegisterType<GameService>().As<IGameService>().InstancePerLifetimeScope();
            builder.RegisterType<CommentService>().As<ICommentService>().InstancePerLifetimeScope();
        }

    }
}