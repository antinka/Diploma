using AutoMapper;
using GameStore.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Infastracture
{
    public class MapperConfigUI
    {
        public static IMapper GetMapper()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DTOToViewModel());
            });

            return mapperConfiguration.CreateMapper();
        }
    }
}