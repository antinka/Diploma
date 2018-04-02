using AutoMapper;

namespace GameStore.BAL.Infastracture
{
    public class MapperConfigBll
    {
        public static IMapper GetMapper()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EntitiToDto());
                cfg.AddProfile(new DtoToEntity());
            });
            return mapperConfiguration.CreateMapper();
        }
    }
}
