using System.Threading;
using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.Web.Authorization;
using GameStore.Web.ViewModels;
using GameStore.Web.ViewModels.Games;

namespace GameStore.Web.Infrastructure.Mapper
{
    public class DtoToViewModel : Profile
    {
        public DtoToViewModel()
        {
            string current;

            CreateMap<UserDTO, UserViewModel>();

            CreateMap<UserDTO, User>().ForMember(dest => dest.Roles ,opt => opt.MapFrom(src => src.Roles));

            CreateMap<RoleDTO, RoleViewModel>();

            CreateMap<CommentDTO, CommentViewModel>();

            CreateMap<PublisherDTO, DetailsPublisherViewModel>();

            CreateMap<ExtendPublisherDTO, PublisherViewModel>();

            CreateMap<ExtendPublisherDTO, DetailsPublisherViewModel>().ForMember(dest => dest.Description, opt => opt.ResolveUsing(src =>
            {
                current = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpperInvariant();

                return current == "EN" ? src.DescriptionEn : src.DescriptionRu;
            }));

            CreateMap<OrderDTO, OrderViewModel>();

            CreateMap<OrderDetailDTO, OrderDetailViewModel>();

            CreateMap<GameDTO, DetailsGameViewModel>();

            CreateMap<GameDTO, FilterGameViewModel>();

            CreateMap<ExtendGameDTO, GameViewModel>();

            CreateMap<ExtendGameDTO, DetailsGameViewModel>().ForMember(dest => dest.Name, opt => opt.ResolveUsing(src =>
            {
                current = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpperInvariant();

                return current == "EN" ? src.NameEn : src.NameRu ?? src.NameEn;
            }))
                .ForMember(dest => dest.Description, opt => opt.ResolveUsing(src =>
                {
                    current = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpperInvariant();

                    return current == "EN" ? src.DescriptionEn : src.DescriptionRu;
                }));

            CreateMap<ExtendGameDTO, FilterGameViewModel>().ForMember(dest => dest.Name, opt => opt.ResolveUsing(src =>
                {
                    current = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpperInvariant();

                    return current == "EN" ? src.NameEn : src.NameRu ?? src.NameEn;
                }))
                .ForMember(dest => dest.Description, opt => opt.ResolveUsing(src =>
                {
                    current = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpperInvariant();

                    return current == "EN" ? src.DescriptionEn : src.DescriptionRu;
                }));

            CreateMap<GenreDTO, DelailsGenreViewModel>();

            CreateMap<ExtendGenreDTO, GenreViewModel>();

            CreateMap<ExtendGenreDTO, DelailsGenreViewModel>().ForMember(dest => dest.Name, opt => opt.ResolveUsing(src =>
            {
                current = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpperInvariant();

                return current == "EN" ? src.NameEn : src.NameRu ?? src.NameEn;
            }));

            CreateMap<PlatformTypeDTO, DetailsPlatformTypeViewModel>();

            CreateMap<ExtendPlatformTypeDTO, PlatformTypeViewModel>();

            CreateMap<ExtendPlatformTypeDTO, DetailsPlatformTypeViewModel>().ForMember(dest => dest.Name, opt => opt.ResolveUsing(src =>
            {
                current = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpperInvariant();

                return current == "EN" ? src.NameEn : src.NameRu ?? src.NameEn;
            }));

            CreateMap<FilterDTO, FilterViewModel>();

            CreateMap<ShipperDTO, ShipperViewModel>();
        }
    }
}