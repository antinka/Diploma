using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.Web.Filters;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Controllers
{
    [TrackRequestIp]
    [ExceptionFilter]
    public class GenreController : BaseController
    {
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;

        public GenreController(IGenreService genreService, IMapper mapper)
        {
            _mapper = mapper;
            _genreService = genreService;
        }

        [HttpGet]
        public ActionResult New()
        {
            var genres = _mapper.Map<IEnumerable<GenreViewModel>>(_genreService.GetAll());

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
                var genreDto = _mapper.Map<GenreDTO>(genreViewModel);

                if (!_genreService.IsUniqueName(genreDto))
                {
                    ModelState.AddModelError("Name", "Genre with such name already exist, please enter another name");
                }

                if (ModelState.IsValid)
                {
                    _genreService.AddNew(genreDto);

                    return RedirectToAction("Get", new { genreName = genreViewModel.Name });
                }
            }

            var genres = _mapper.Map<IEnumerable<GenreViewModel>>(_genreService.GetAll());
            genreViewModel.GenreList = new SelectList(genres, "Id", "Name");

            return View(genreViewModel);
        }

        [HttpGet]
        public ActionResult Get(string genreName)
        {
            var genreDto = _genreService.GetByName(genreName);
            var genreViewModel = _mapper.Map<GenreViewModel>(genreDto);

            return View(genreViewModel);
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var genresDto = _genreService.GetAll();
            var genresViewModel = _mapper.Map<IEnumerable<GenreViewModel>>(genresDto);

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

            var genres = _mapper.Map<IEnumerable<GenreViewModel>>(_genreService.GetAll());
            genreViewModel.GenreList = new SelectList(genres, "Id", "Name");

            return View(genreViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GenreViewModel genreViewModel)
        {
            if (ModelState.IsValid)
            {
                var genreDto = _mapper.Map<GenreDTO>(genreViewModel);

                if (genreViewModel.ParentGenreId != null)
                {

                    if (!_genreService.IsPossibleRelation(genreDto))
                        ModelState.AddModelError("", "such relation could not exist");
                }

                if (!_genreService.IsUniqueName(genreDto))
                {
                    ModelState.AddModelError("Name", "Genre with such name already exist, please enter another name");
                }

                if (ModelState.IsValid)
                {
                    _genreService.Update(genreDto);

                    return RedirectToAction("GetAll");
                }
            }

            var genres = _mapper.Map<IEnumerable<GenreViewModel>>(_genreService.GetAll());
            genreViewModel.GenreList = new SelectList(genres, "Id", "Name");

            return View(genreViewModel);
        }
    }
}