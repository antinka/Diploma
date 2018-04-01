using AutoMapper;

namespace GameStore.Infastracture
{
    public class MapperConfigUi
    {
        public static IMapper GetMapper()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DtoToViewModel());
            });

            return mapperConfiguration.CreateMapper();
        }
    }
}