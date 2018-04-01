using AutoMapper;
using GameStore.BAL.DTO;
using GameStore.DAL.Entities;

namespace GameStore.BAL.Infastracture
{
    public class EntitiToDto : Profile
    {
        public EntitiToDto()
        {
            CreateMap<GameDTO, Game>();
            CreateMap<Game, GameDTO>() ;

            CreateMap<GenreDTO, Genre>();
            CreateMap<Genre, GenreDTO>();

            CreateMap<PlatformTypeDTO, PlatformType>();
            CreateMap<PlatformType, PlatformTypeDTO>();

            CreateMap<CommentDTO, Comment>();
            CreateMap<Comment, CommentDTO>();
        }
    }
}