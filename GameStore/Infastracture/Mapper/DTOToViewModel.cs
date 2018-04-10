using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.ViewModels;

namespace GameStore.Infastracture
{
    public class DtoToViewModel : Profile
    {
        public DtoToViewModel()
        {
            CreateMap<GameDTO, GameViewModel>();

            CreateMap<GenreDTO, GenreViewModel>();

            CreateMap<PlatformTypeDTO, PlatformTypeViewModel>();

            CreateMap<CommentDTO, CommentViewModel>();
        }
    }
}