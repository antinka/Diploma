﻿using Autofac;
using GameStore.DAL.EF;
using GameStore.DAL.Interfaces;

namespace GameStore.BLL.Infastracture
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
            builder.RegisterType<GameStoreContext>().As<IDbContext>().InstancePerLifetimeScope().WithParameter("connectionString", _connectionString);
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
        }
    }
}
