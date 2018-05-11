using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GameStore.Controllers
{
    public class PublisherController : Controller
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
        public ActionResult New(PublisherViewModel publisher)
        {
            if (ModelState.IsValid)
            {
                var publisherDTO = _mapper.Map<PublisherDTO>(publisher);
                var isAddNewPublisher = _publisherService.AddNew(publisherDTO);

                if (isAddNewPublisher)
                    return RedirectToAction("Get", new { companyName = publisher.Name });

                ModelState.AddModelError("Name", "Publisher with such name already exist, please enter another name");
            }

            return View(publisher);
        }

        [HttpGet]
        public ActionResult Get(string companyName)
        {
            var publisherDTO = _publisherService.GetByName(companyName);
            var publisherViewModel = _mapper.Map<PublisherViewModel>(publisherDTO);

            return View(publisherViewModel);
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var publishersDTO = _publisherService.GetAll();
            var publisherViewModel = _mapper.Map<IEnumerable<PublisherViewModel>>(publishersDTO);

            return View(publisherViewModel);
        }

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
        public ActionResult Update(PublisherViewModel publisherViewModel)
        {
            if (ModelState.IsValid)
            {
                var publisherDTO = _mapper.Map<PublisherDTO>(publisherViewModel);
                _publisherService.Update(publisherDTO);

                return RedirectToAction("GetAll");
            }

            return View(publisherViewModel);
        }
    }
}