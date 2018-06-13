using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using AutoMapper;
using GameStore.BLL.Enums;
using GameStore.BLL.Interfaces;
using GameStore.Web.Builder.Interfaces;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Builder.Implementation
{
    public class FilterViewModelBuilder : IGenericBuilder<FilterViewModel>
    {
        private readonly IGenreService _genreService;
        private readonly IPlatformTypeService _platformTypeService;
        private readonly IPublisherService _publisherService;
        private readonly IMapper _mapper;

        public FilterViewModelBuilder(
            IGenreService genreService,
            IPlatformTypeService platformTypeService,
            IMapper mapper,
            IPublisherService publisherService)
        {
            _mapper = mapper;
            _publisherService = publisherService;
            _genreService = genreService;
            _platformTypeService = platformTypeService;
        }

        public FilterViewModel Build()
        {
            var model = new FilterViewModel();
            var genresDto = _genreService.GetAll();
            var genrelist = _mapper.Map<IEnumerable<DelailsGenreViewModel>>(genresDto);
            var platformlist = _mapper.Map<IEnumerable<DetailsPlatformTypeViewModel>>(_platformTypeService.GetAll());
            var publisherlist = _mapper.Map<IEnumerable<DetailsPublisherViewModel>>(_publisherService.GetAll());

            var listGenreBoxs = new List<CheckBox>();
            genrelist.ToList().ForEach(genre => listGenreBoxs.Add(new CheckBox() { Text = genre.Name }));
            model.ListGenres = listGenreBoxs;

            var listPlatformBoxs = new List<CheckBox>();
            platformlist.ToList().ForEach(platform => listPlatformBoxs.Add(new CheckBox() { Text = platform.Name }));
            model.ListPlatformTypes = listPlatformBoxs;

            var listPublisherBoxs = new List<CheckBox>();
            publisherlist.ToList().ForEach(publisher => listPublisherBoxs.Add(new CheckBox() { Text = publisher.Name }));
            model.ListPublishers = listPublisherBoxs;

            return model;
        }

        public FilterViewModel Rebuild(FilterViewModel filterViewModel)
        {
            var model = Build();

            if (filterViewModel.SelectedGenresName != null)
            {
                model.SelectedGenres = model.ListGenres.Where(x => filterViewModel.SelectedGenresName.Contains(x.Text));
            }

            if (filterViewModel.SelectedPlatformTypesName != null)
            {
                model.SelectedPlatformTypes = model.ListPlatformTypes
                    .Where(x => filterViewModel.SelectedPlatformTypesName.Contains(x.Text));
            }

            if (filterViewModel.SelectedPublishersName != null)
            {
                model.SelectedPublishers = model.ListPublishers
                    .Where(x => filterViewModel.SelectedPublishersName.Contains(x.Text));
            }

            model.PagingInfo = filterViewModel.PagingInfo;

            return model;
        }
    }
}