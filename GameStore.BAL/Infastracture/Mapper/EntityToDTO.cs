using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Infastracture
{
    public class EntityToDto : Profile
    {
        public EntityToDto()
        {
            CreateMap<Comment, CommentDTO>();

            CreateMap<Order, OrderDTO>();

            CreateMap<OrderDetail, OrderDetailDTO>();

            CreateMap<Publisher, PublisherDTO>();

            CreateMap<Game, GameDTO>() ;

            CreateMap<Genre, GenreDTO>();

            CreateMap<PlatformType, PlatformTypeDTO>();
        }
    }
}