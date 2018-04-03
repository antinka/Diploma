using AutoMapper;
using GameStore.BAL.DTO;
using GameStore.DAL.Entities;

namespace GameStore.BAL.Infastracture
{
    public class DtoToEntity : Profile
    {
        public DtoToEntity()
        {
            CreateMap<GameDTO, Game>();
        
            CreateMap<GenreDTO, Genre>();

            CreateMap<PlatformTypeDTO, PlatformType>();
       
            CreateMap<CommentDTO, Comment>();
        }
    }
}