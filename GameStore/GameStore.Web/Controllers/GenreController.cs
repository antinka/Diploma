using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.Web.App_LocalResources;
using GameStore.Web.Authorization.Interfaces;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Controllers
{
    [Authorize(Roles = "Manager")]
    public class GenreController : BaseController
    {
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;

        public GenreController(
            IGenreService genreService,
            IMapper mapper,
            IAuthentication authentication) : base(authentication)
        {
            _mapper = mapper;
            _genreService = genreService;
        }

        [HttpGet]
        public ActionResult New()
        {
            var genres = _mapper.Map<IEnumerable<DelailsGenreViewModel>>(_genreService.GetAll());

            var genreViewModel = new GenreViewModel()
            {
                GenreList = new SelectList(genres, "Id", "Name"),
            };

            return View(genreViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(GenreViewModel genreViewModel)
        {
            if (ModelState.IsValid)
            {
                var genreDto = _mapper.Map<ExtendGenreDTO>(genreViewModel);

                if (!_genreService.IsUniqueEnName(genreDto))
                {
                    ModelState.AddModelError("NameEn", GlobalRes.ExistGenreName);
                }

                if (genreViewModel.NameRu != null)
                {
                    if (!_genreService.IsUniqueRuName(genreDto))
                    {
                        ModelState.AddModelError("NameRu", GlobalRes.ExistGenreName);
                    }
                }

                if (ModelState.IsValid)
                {
                    _genreService.AddNew(genreDto);

                    return RedirectToAction("GetAll");
                }
            }

            var genres = _mapper.Map<IEnumerable<DelailsGenreViewModel>>(_genreService.GetAll());
            genreViewModel.GenreList = new SelectList(genres, "Id", "Name");

            return View(genreViewModel);
        }

        [HttpGet]
        public ActionResult Get(string genreName)
        {
            var genreDto = _genreService.GetByName(genreName);
            var genreViewModel = _mapper.Map<DelailsGenreViewModel>(genreDto);

            return View(genreViewModel);
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var genresDto = _genreService.GetAll();
            var genresViewModel = _mapper.Map<IEnumerable<DelailsGenreViewModel>>(genresDto);

            return View(genresViewModel);
        }

        [HttpPost]
        public ActionResult Remove(Guid genreId)
        {
            _genreService.Delete(genreId);

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public ActionResult Update(string genreName)
        {
            var genreDto = _genreService.GetByName(genreName);
            var genreViewModel = _mapper.Map<GenreViewModel>(genreDto);

            var genres = _mapper.Map<IEnumerable<DelailsGenreViewModel>>(_genreService.GetAll());
            genreViewModel.GenreList = new SelectList(genres, "Id", "Name");

            return View(genreViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GenreViewModel genreViewModel)
        {
            if (ModelState.IsValid)
            {
                var genreDto = _mapper.Map<ExtendGenreDTO>(genreViewModel);

                if (genreViewModel.ParentGenreId != null)
                {
                    if (!_genreService.IsPossibleRelation(genreDto))
                    {
                        ModelState.AddModelError(string.Empty, GlobalRes.ExistGenreRelation);
                    }
                }

                if (!_genreService.IsUniqueEnName(genreDto))
                {
                    ModelState.AddModelError("NameEn", GlobalRes.ExistGenreName);
                }

                if (genreViewModel.NameRu != null)
                {
                    if (!_genreService.IsUniqueRuName(genreDto))
                    {
                        ModelState.AddModelError("NameRu", GlobalRes.ExistGenreName);
                    }
                }

                if (ModelState.IsValid)
                {
                    _genreService.Update(genreDto);

                    return RedirectToAction("GetAll");
                }
            }

            var genres = _mapper.Map<IEnumerable<DelailsGenreViewModel>>(_genreService.GetAll());
            genreViewModel.GenreList = new SelectList(genres, "Id", "Name");

            return View(genreViewModel);
        }
    }
}