using AutoMapper;
using GameStore.BAL.DTO;
using GameStore.Models;

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