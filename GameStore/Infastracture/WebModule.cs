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
        }
    }
}