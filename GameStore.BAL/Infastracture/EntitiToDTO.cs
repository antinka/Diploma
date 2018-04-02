using AutoMapper;
using GameStore.BAL.DTO;
using GameStore.DAL.Entities;

namespace GameStore.BAL.Infastracture
{
    public class EntitiToDto : Profile
    {
        public EntitiToDto()
        {
            CreateMap<Game, GameDTO>() ;

            CreateMap<Genre, GenreDTO>();

            CreateMap<PlatformType, PlatformTypeDTO>();

            CreateMap<Comment, CommentDTO>();
        }
    }
}