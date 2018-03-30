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
    public class BLLModule: Autofac.Module
    {
        string connectionString;

        public BLLModule(string connectionString)
        {
            this.connectionString = connectionString;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GameStoreContext>().As<IDbContext>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest().WithParameter("connectionString", this.connectionString);
        }
    }
}
