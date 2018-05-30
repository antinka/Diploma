using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.Web.ViewModels;
using GameStore.Web.ViewModels.Games;

namespace GameStore.Web.Infrastructure.Mapper
{
    public class ViewModelToDto : Profile
    {
        public ViewModelToDto()
        {
            CreateMap<UserViewModel, UserDTO>();

            CreateMap<RoleViewModel, RoleDTO>();

            CreateMap<CommentViewModel, CommentDTO>();

            CreateMap<PublisherViewModel, ExtendPublisherDTO>();

            CreateMap<OrderViewModel, OrderDTO>();

            CreateMap<OrderDetailViewModel, OrderDetailDTO>();

            CreateMap<DetailsGameViewModel, GameDTO>();

            CreateMap<FilterGameViewModel, GameDTO>();

            CreateMap<GameViewModel, ExtendGameDTO>();

            CreateMap<GenreViewModel, ExtendGenreDTO>();

            CreateMap<DetailsPlatformTypeViewModel, PlatformTypeDTO>();

            CreateMap<PlatformTypeViewModel, ExtendPlatformTypeDTO>();

            CreateMap<FilterViewModel, FilterDTO>();

            CreateMap<ShipperViewModel, ShipperDTO>();
        }
    }
}