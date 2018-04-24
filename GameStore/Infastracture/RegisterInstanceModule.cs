using Autofac;
using log4net;

namespace GameStore.Infastracture
{
    public class RegisterInstanceModule : Module
    {//todo could be moved to web module
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(LogManager.GetLogger("LOGGER"));
            builder.RegisterInstance(MapperConfigUi.GetMapper().CreateMapper());
        }
    }
}