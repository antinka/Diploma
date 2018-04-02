using Autofac;
using GameStore.DAL.EF;
using GameStore.DAL.Interfaces;

namespace GameStore.BAL.Infastracture
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
            builder.RegisterInstance(MapperConfigBll.GetMapper());
            builder.RegisterType<GameStoreContext>().As<IDbContext>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest().WithParameter("connectionString", _connectionString);
        }
    }
}
