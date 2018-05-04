using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.DAL.Entities;
using GameStore.DAL.Mongo.MongoEntities;

namespace GameStore.BLL.Infrastructure.Mapper
{
    public class DtoToEntity : Profile
    {
        public DtoToEntity()
        {
            CreateMap<CommentDTO, Comment>();

            CreateMap<OrderDTO, Order>();

            CreateMap<OrderDetailDTO, OrderDetail>();

            CreateMap<PublisherDTO, Publisher>();

            CreateMap<GameDTO, Game>();
        
            CreateMap<GenreDTO, Genre>();

            CreateMap<PlatformTypeDTO, PlatformType>();

            CreateMap<ShipperDTO, Shipper>();
        }
    }
}