﻿using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.DAL.Entities;
using GameStore.DAL.Mongo.MongoEntities;

namespace GameStore.BLL.Infrastructure.Mapper
{
    public class EntityToDto : Profile
    {
        public EntityToDto()
        {
            CreateMap<Comment, CommentDTO>();

            CreateMap<Order, OrderDTO>();

            CreateMap<OrderDetail, OrderDetailDTO>();

            CreateMap<Publisher, PublisherDTO>();

            CreateMap<Game, GameDTO>() ;

            CreateMap<Genre, GenreDTO>();

            CreateMap<PlatformType, PlatformTypeDTO>();

            CreateMap<Shipper, ShipperDTO>();
        }
    }
}