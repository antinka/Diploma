using AutoMapper;
using GameStore.BAL.DTO;
using GameStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BAL
{
    public static class AutomapperDalBal
    {
        public static bool ExistMapBalDal = false;

        public static void CreateMap()
        {

            //{
            //    Mapper.Initialize(cfg => cfg.CreateMap<GameDTO, Game>()
            //    );
            //             //.ForMember(x => x.Genres, x => x.MapFrom(c => c.Genres))
            //             //.ForMember(x => x.PlatformTypes, x => x.MapFrom(c => c.PlatformTypes))
            //             //.ForMember(x => x.Comments, x => x.MapFrom(c => c.Comments)));
            //      Mapper.Initialize(cfg => cfg.CreateMap<CommentDTO, Comment>());

            //    Mapper.Initialize(cfg => cfg.CreateMap<GenreDTO, Genre>());
            //    //.ForMember(x => x.Games, x => x.MapFrom(c => c.Games)));

            //    Mapper.Initialize(cfg => cfg.CreateMap<PlatformTypeDTO, PlatformType>());
            //    //.ForMember(x => x.Games, x => x.MapFrom(c => c.Games)));
            //    ExistMapBalDal = true;
        }
        //public static void CreateCommentMap()
        //{
        //    Mapper.Initialize(cfg => cfg.CreateMap<CommentDTO, Comment>());
        //}
    }
}