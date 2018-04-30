using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.ViewModels;

namespace GameStore.Infastracture
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