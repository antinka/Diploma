using AutoMapper;
using GameStore.BAL.DTO;
using GameStore.Models;

namespace GameStore.Infastracture
{
    public class DtoToViewModel : Profile
        {
            public DtoToViewModel()
            {
                CreateMap<GameViewModel, GameDTO>();
                CreateMap<GameDTO, GameViewModel>();

                CreateMap<GenreViewModel, GenreDTO>();
                CreateMap<GenreDTO, GenreViewModel>();

                CreateMap<PlatformTypeViewModel, PlatformTypeDTO>();
                CreateMap<PlatformTypeDTO, PlatformTypeViewModel>();

                CreateMap<CommentViewModel, CommentDTO>();
                CreateMap<CommentDTO, CommentViewModel>();
            }
        }
}