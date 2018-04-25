using Autofac;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Service;
using log4net;

namespace GameStore.Infastracture
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(LogManager.GetLogger("LOGGER"));
            builder.RegisterInstance(MapperConfigUi.GetMapper().CreateMapper());
            builder.RegisterType<GameService>().As<IGameService>().InstancePerLifetimeScope();
            builder.RegisterType<CommentService>().As<ICommentService>().InstancePerLifetimeScope();
            builder.RegisterType<PublisherService>().As<IPublisherService>().InstancePerLifetimeScope();
            builder.RegisterType<OrdersService>().As<IOrdersService>().InstancePerLifetimeScope();
            builder.RegisterType<GenreService>().As<IGenreService>().InstancePerLifetimeScope();
            builder.RegisterType<PlatformTypeService>().As<IPlatformTypeService>().InstancePerLifetimeScope();
        }
    }
}