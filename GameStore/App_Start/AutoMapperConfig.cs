using AutoMapper;
using GameStore.BAL.DTO;
using GameStore.DAL.Entities;
using GameStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.App_Start
{
    public static class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<GameViewModel, GameDTO>()
            .ForMember(x => x.Genres, x => x.MapFrom(c => c.Genres))
            .ForMember(x => x.PlatformTypes, x => x.MapFrom(c => c.PlatformTypes))
            .ForMember(x => x.Comments, x => x.MapFrom(c => c.Comments)));

            //Mapper.Initialize(cfg => cfg.CreateMap<CommentViewModel, CommentDTO>());

            //Mapper.Initialize(cfg => cfg.CreateMap<GenreViewModel, GenreDTO>()
            //.ForMember(x => x.Games, x => x.MapFrom(c => c.Games)));

            //Mapper.Initialize(cfg => cfg.CreateMap<PlatformTypeViewModel, PlatformTypeDTO>()
            //.ForMember(x => x.Games, x => x.MapFrom(c => c.Games)));
        }
    }
}