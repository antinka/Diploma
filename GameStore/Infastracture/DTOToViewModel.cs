using Autofac;
using AutoMapper;
using GameStore.BAL.DTO;
using GameStore.DAL.Entities;
using GameStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Infastracture
{
        public class DTOToViewModel : Profile
        {
            public DTOToViewModel()
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