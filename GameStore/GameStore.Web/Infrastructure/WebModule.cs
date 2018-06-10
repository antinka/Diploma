using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using GameStore.Web.Authorization.Implementation;
using GameStore.Web.Authorization.Interfaces;
using GameStore.Web.Builder.Implementation;
using GameStore.Web.Filters;
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
            builder.Register(pf => new TrackRequestIp(pf.Resolve<ILog>())).AsActionFilterFor<Controller>();
            builder.Register(pf => new ExceptionFilter(pf.Resolve<ILog>())).AsExceptionFilterFor<Controller>();
            builder.RegisterType<FilterViewModelBuilder>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterInstance(MapperConfigUi.GetMapper().CreateMapper());
            builder.RegisterType<Bank>().As<IPayment>().InstancePerLifetimeScope();
            builder.RegisterType<Visa>().As<IPayment>().InstancePerLifetimeScope();
            builder.RegisterType<Box>().As<IPayment>().InstancePerLifetimeScope();
            builder.RegisterType<PaymentStrategy>().As<IPaymentStrategy>().InstancePerLifetimeScope();
            builder.RegisterType<CustomAuthentication>().As<IAuthentication>().InstancePerLifetimeScope();
        }
    }
}