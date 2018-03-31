using AutoMapper;
using AutoMapper.EntityFramework;
using AutoMapper.EquivalencyExpression;
using GameStore.BAL;
using GameStore.BAL.DTO;
using GameStore.DAL.Entities;
using System.Collections.Generic;

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
