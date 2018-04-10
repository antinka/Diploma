using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Infastracture
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