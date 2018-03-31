using Autofac;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using GameStore.BAL.DTO;
using GameStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GameStore.BAL
{
    public class EntitiToDTO : Profile
    {
        public EntitiToDTO()
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