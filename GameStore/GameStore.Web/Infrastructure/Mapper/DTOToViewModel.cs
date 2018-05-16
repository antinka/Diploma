using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Infrastructure.Mapper
{
    public class DtoToViewModel : Profile
    {
        public DtoToViewModel()
        {
            CreateMap<CommentDTO, CommentViewModel>();

            CreateMap<PublisherDTO, PublisherViewModel>();

            CreateMap<OrderDTO, OrderViewModel>();

            CreateMap<OrderDetailDTO, OrderDetailViewModel>();

            CreateMap<GameDTO, GameViewModel>();

            CreateMap<GenreDTO, GenreViewModel>();

            CreateMap<PlatformTypeDTO, PlatformTypeViewModel>();

            CreateMap<FilterDTO, FilterViewModel>();
        }
    }
}