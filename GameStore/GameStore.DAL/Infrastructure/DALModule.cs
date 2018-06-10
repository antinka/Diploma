using Autofac;
using GameStore.DAL.EF;
using GameStore.DAL.Interfaces;

namespace GameStore.DAL.Infrastructure
{
    public class DALModule : Module
    {
        private readonly string _connectionString;

        public DALModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GameStoreDBContext>().As<IDbContext>().InstancePerLifetimeScope().WithParameter("connectionString", _connectionString);
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
        }
    }
}