using AutoMapper;
using GameStore.BAL.DTO;
using GameStore.Models;

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