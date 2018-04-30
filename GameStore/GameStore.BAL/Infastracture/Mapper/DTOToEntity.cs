using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Infastracture
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
        }
    }
}