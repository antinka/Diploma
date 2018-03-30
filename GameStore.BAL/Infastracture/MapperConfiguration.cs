using AutoMapper;
using GameStore.BAL;

namespace GameStore.Infastracture
{
    public class MapperConfigBLL
    {
        public static IMapper GetMapper()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EntitiToDTO());
            });
            return mapperConfiguration.CreateMapper();
        }
    }
}
