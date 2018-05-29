using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.Web.App_LocalResources;
using GameStore.Web.Filters;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Controllers
{
    [TrackRequestIp]
    [ExceptionFilter]
    public class PublisherController : BaseController
    {
        private readonly IPublisherService _publisherService;
        private readonly IMapper _mapper;

        public PublisherController(IPublisherService publisherService, IMapper mapper)
        {
            _publisherService = publisherService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(PublisherViewModel publisher)
        {
            if (ModelState.IsValid)
            {
                var publisherDTO = _mapper.Map<ExtendPublisherDTO>(publisher);

                var isUniqueName = _publisherService.IsUniqueName(publisherDTO);

                if (isUniqueName)
                { 
                    _publisherService.AddNew(publisherDTO);

                    return RedirectToAction("GetAll");
                }

                ModelState.AddModelError("Name", GlobalRes.ExistPublisherName);
            }

            return View(publisher);
        }

        [HttpGet]
        public ActionResult Get(string companyName)
        {
            var publisherDTO = _publisherService.GetByName(companyName);
            var publisherViewModel = _mapper.Map<DetailsPublisherViewModel>(publisherDTO);

            return View(publisherViewModel);
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var publishersDTO = _publisherService.GetAll();
            var publisherViewModel = _mapper.Map<IEnumerable<DetailsPublisherViewModel>>(publishersDTO);

            return View(publisherViewModel);
        }

        [HttpPost]
        public ActionResult Remove(Guid publisherId)
        {
            _publisherService.Delete(publisherId);

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public ActionResult Update(string companyName)
        {
            var publisherDTO = _publisherService.GetByName(companyName);
            var publisherViewModel = _mapper.Map<PublisherViewModel>(publisherDTO);

            return View(publisherViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(PublisherViewModel publisherViewModel)
        {
            if (ModelState.IsValid)
            {
                var publisherDTO = _mapper.Map<ExtendPublisherDTO>(publisherViewModel);

                if (!_publisherService.IsUniqueName(publisherDTO))
                {
                    ModelState.AddModelError("Name", GlobalRes.ExistPublisherName);
                }

                if (ModelState.IsValid)
                {
                    _publisherService.Update(publisherDTO);

                    return RedirectToAction("GetAll");
                }
            }

            return View(publisherViewModel);
        }
    }
}