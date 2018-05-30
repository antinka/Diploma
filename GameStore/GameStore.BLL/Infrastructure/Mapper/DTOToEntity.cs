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

            CreateMap<ExtendPublisherDTO, Publisher>();

            CreateMap<ExtendGameDTO, Game>();

            CreateMap<GameDTO, Game>();

            CreateMap<ExtendGenreDTO, Genre>();

            CreateMap<ExtendPlatformTypeDTO, PlatformType>();

            CreateMap<ShipperDTO, Shipper>();
        }
    }
}