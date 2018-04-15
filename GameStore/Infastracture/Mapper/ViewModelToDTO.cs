using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.ViewModels;

namespace GameStore.Infastracture
{
    public class ViewModelToDto : Profile
    {
        public ViewModelToDto()
        {
            CreateMap<GameViewModel, GameDTO>();

            CreateMap<GenreViewModel, GenreDTO>();

            CreateMap<PlatformTypeViewModel, PlatformTypeDTO>();

            CreateMap<CommentViewModel, CommentDTO>();
        }
    }
}