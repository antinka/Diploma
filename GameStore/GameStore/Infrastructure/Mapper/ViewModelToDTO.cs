﻿using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.ViewModels;

namespace GameStore.Infrastructure.Mapper
{
    public class ViewModelToDto : Profile
    {
        public ViewModelToDto()
        {
            CreateMap<CommentViewModel, CommentDTO>();

            CreateMap<PublisherViewModel, PublisherDTO>();

            CreateMap<OrderViewModel, OrderDTO>();

            CreateMap<OrderDetailViewModel, OrderDetailDTO>();

            CreateMap<GameViewModel, GameDTO>();

            CreateMap<GenreViewModel, GenreDTO>();

            CreateMap<PlatformTypeViewModel, PlatformTypeDTO>();

            CreateMap<FilterViewModel, FilterDTO>();

            CreateMap<ShipperViewModel, ShipperDTO>();
        }
    }
}