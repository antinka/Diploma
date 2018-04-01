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
            });
            return mapperConfiguration.CreateMapper();
        }
    }
}
