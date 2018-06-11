using AutoMapper;
using GameStore.DAL.Entities;
using GameStore.DAL.Mongo.MongoEntities;

namespace GameStore.BLL.Infrastructure.Mapper
{
    public class MongoEntityToSqlEntity : Profile
    {
        public MongoEntityToSqlEntity()
        {
            CreateMap<MongoOrder, Order>().ForMember("Date", opt => opt.MapFrom(src => src.OrderDate));
        }
    }
}
