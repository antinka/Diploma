using Autofac;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Service;
using GameStore.Web.Infrastructure.Mapper;
using GameStore.Web.Payments;
using log4net;

namespace GameStore.Web.Infrastructure
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
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<RoleService>().As<IRoleService>().InstancePerLifetimeScope();
            builder.RegisterType<Bank>().As<IPayment>().InstancePerLifetimeScope();
            builder.RegisterType<Visa>().As<IPayment>().InstancePerLifetimeScope();
            builder.RegisterType<Box>().As<IPayment>().InstancePerLifetimeScope();
            builder.RegisterType<PaymentStrategy>().As<IPaymentStrategy>().InstancePerLifetimeScope();
        }
    }
}