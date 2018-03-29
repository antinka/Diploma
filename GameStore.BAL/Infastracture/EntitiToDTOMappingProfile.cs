using Autofac;
using AutoMapper;
using GameStore.BAL.DTO;
using GameStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GameStore.BAL
{
    public class EntitiToDTOMappingProfile : Profile
    {
        public EntitiToDTOMappingProfile()
        {
            CreateMap<GameDTO, Game>();
            CreateMap<Game, GameDTO>();

            CreateMap<GenreDTO, Genre>();
            CreateMap<Genre, GenreDTO>();

            CreateMap<PlatformTypeDTO, PlatformType>();
            CreateMap<PlatformType, PlatformTypeDTO>();

            CreateMap<CommentDTO, CommentDTO>();
            CreateMap<CommentDTO, CommentDTO>();
        }
    }

    public class AutoMapperModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //register all profile classes in the calling assembly
            builder.RegisterAssemblyTypes(typeof(AutoMapperModule).Assembly).As<Profile>();

            builder.Register(context => new MapperConfiguration(cfg =>
            {
                foreach (var profile in context.Resolve<IEnumerable<Profile>>())
                {
                    cfg.AddProfile(profile);
                }
            })).AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();
        }
    }
}