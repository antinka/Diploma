using Autofac;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Service;

namespace GameStore.Infastracture
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GameService>().As<IGameService>().InstancePerLifetimeScope();
            builder.RegisterType<CommentService>().As<ICommentService>().InstancePerLifetimeScope();
            builder.RegisterType<PublisherService>().As<IPublisherService>().InstancePerLifetimeScope();
            builder.RegisterType<OrdersService>().As<IOrdersService>().InstancePerLifetimeScope();
            builder.RegisterType<GenreService>().As<IGenreService>().InstancePerLifetimeScope();
            builder.RegisterType<PlatformTypeService>().As<IPlatformTypeService>().InstancePerLifetimeScope();
        }
    }
}