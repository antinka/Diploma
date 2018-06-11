using Autofac;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Service;
using GameStore.DAL.EF;
using GameStore.DAL.Infrastructure;
using GameStore.DAL.Interfaces;

namespace GameStore.BLL.Infrastructure
{
    public class BllModule : Module
    {
        private readonly string _connectionString;

        public BllModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GameService>().As<IGameService>().InstancePerLifetimeScope();
            builder.RegisterType<CommentService>().As<ICommentService>().InstancePerLifetimeScope();
            builder.RegisterType<PublisherService>().As<IPublisherService>().InstancePerLifetimeScope();
            builder.RegisterType<OrdersService>().As<IOrdersService>().InstancePerLifetimeScope();
            builder.RegisterType<GenreService>().As<IGenreService>().InstancePerLifetimeScope();
            builder.RegisterType<PlatformTypeService>().As<IPlatformTypeService>().InstancePerLifetimeScope();
            builder.RegisterModule(new DALModule(_connectionString));
        }
    }
}
