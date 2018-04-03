﻿using Autofac;
using Autofac.Integration.Mvc;
using GameStore.BLL.Infastracture;
using log4net;
using System.Reflection;
using System.Web.Mvc;

namespace GameStore.Infastracture
{
    public class AutofacConfig
    {
        public static void Setup()
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var builder = new ContainerBuilder();

            RegisterDependencies(currentAssembly, builder);

            var container = builder.Build();
            var resolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(resolver);
        }

        public static void RegisterDependencies(Assembly currentAssembly, ContainerBuilder builder)
        {
            // Register your MVC controllers.
            builder.RegisterControllers(currentAssembly);

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();
            builder.RegisterModule(new WebModule());
            builder.RegisterModule(new BllModule("DefaultConnection"));
            builder.RegisterModule(new RegisterInstanceModule());
        }

    }
}