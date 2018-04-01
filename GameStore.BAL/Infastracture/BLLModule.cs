using Autofac;
using GameStore.BAL.Interfaces;
using GameStore.BAL.Service;
using GameStore.DAL.EF;
using GameStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BAL.Infastracture
{
    public class BllModule: Autofac.Module
    {
        private readonly string _connectionString;

        public BllModule(string connectionString)
        {
            _connectionString = connectionString;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GameStoreContext>().As<DbContext>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWorkGeneric>().As<IUnitOfWorkGeneric>().InstancePerRequest().WithParameter("connectionString",_connectionString);
        }
    }
}
