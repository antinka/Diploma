using AutoMapper;
using GameStore.BLL.Infastracture;

namespace GameStore.Infastracture
{
    public class MapperConfigUi
    {
        public static MapperConfiguration GetMapper()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DtoToViewModel());
                cfg.AddProfile(new ViewModelToDto());
                cfg.AddProfile(new EntityToDto());
                cfg.AddProfile(new DtoToEntity());
            });

            return mapperConfiguration;
        }
    }
}