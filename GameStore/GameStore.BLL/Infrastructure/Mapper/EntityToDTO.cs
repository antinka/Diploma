using System.Threading;
using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.DAL.Entities;
using GameStore.DAL.Mongo.MongoEntities;

namespace GameStore.BLL.Infrastructure.Mapper
{
    public class EntityToDto : Profile
    {
        public EntityToDto()
        {
            string current;

            CreateMap<Comment, CommentDTO>();

            CreateMap<Order, OrderDTO>();

            CreateMap<OrderDetail, OrderDetailDTO>();

            CreateMap<Publisher, PublisherDTO>().ForMember(
                dest => dest.Description,
                opt => opt.ResolveUsing(src =>
            {
                current = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpperInvariant();

                return current == "EN" ? src.DescriptionEn : src.DescriptionRu;
            }));

            CreateMap<Publisher, ExtendPublisherDTO>();

            CreateMap<Game, GameDTO>().ForMember(
                dest => dest.Name,
                opt => opt.ResolveUsing(src =>
            {
                current = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpperInvariant();

                return current == "EN" ? src.NameEn : src.NameRu ?? src.NameEn;
            }))
                .ForMember(
                dest => dest.Description,
                opt => opt.ResolveUsing(src =>
                {
                    current = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpperInvariant();

                    return current == "EN" ? src.DescriptionEn : src.DescriptionRu;
                }));

            CreateMap<Game, ExtendGameDTO>();

            CreateMap<Genre, GenreDTO>().ForMember(
                dest => dest.Name,
                opt => opt.ResolveUsing(src =>
            {
                current = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpperInvariant();

                return current == "EN" ? src.NameEn : src.NameRu ?? src.NameEn;
            }));

            CreateMap<Genre, ExtendGenreDTO>();

            CreateMap<PlatformType, PlatformTypeDTO>().ForMember(
                dest => dest.Name,
                opt => opt.ResolveUsing(src =>
            {
                current = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpperInvariant();

                return current == "EN" ? src.NameEn : src.NameRu ?? src.NameEn;
            }));

            CreateMap<PlatformType, ExtendPlatformTypeDTO>();

            CreateMap<Shipper, ShipperDTO>();
        }
    }
}