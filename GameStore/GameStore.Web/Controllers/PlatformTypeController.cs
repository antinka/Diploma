﻿using System;
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
    public class PlatformTypeController : Controller
    {
        private readonly IPlatformTypeService _platformTypeService;
        private readonly IMapper _mapper;

        public PlatformTypeController(IPlatformTypeService platformTypeService, IMapper mapper)
        {
            _mapper = mapper;
            _platformTypeService = platformTypeService;
        }

        [HttpGet]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(PlatformTypeViewModel platformTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                var platformTypeDTO = _mapper.Map<PlatformTypeDTO>(platformTypeViewModel);

                if (!_platformTypeService.IsUniqueName(platformTypeDTO))
                {
                    ModelState.AddModelError("Name", "Platform type with such name already exist, please enter another name");
                }

                if (ModelState.IsValid)
                {
                    _platformTypeService.AddNew(platformTypeDTO);

                    return RedirectToAction("Get", new { platformTypeName = platformTypeViewModel.Name });
                }
            }

            return View(platformTypeViewModel);
        }

        [HttpGet]
        public ActionResult Get(string platformTypeName)
        {
            var platformTypeDTO = _platformTypeService.GetByName(platformTypeName);
            var platformTypeViewModel = _mapper.Map<PlatformTypeViewModel>(platformTypeDTO);

            return View(platformTypeViewModel);
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var platformTypesDto = _platformTypeService.GetAll();
            var platformTypesViewModel = _mapper.Map<IEnumerable<PlatformTypeViewModel>>(platformTypesDto);

            return View(platformTypesViewModel);
        }

        [HttpPost]
        public ActionResult Remove(Guid platformTypeId)
        {
            _platformTypeService.Delete(platformTypeId);

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public ActionResult Update(string platformTypeName)
        {
            var platformTypeDTO = _platformTypeService.GetByName(platformTypeName);
            var platformTypeViewModel = _mapper.Map<PlatformTypeViewModel>(platformTypeDTO);

            return View(platformTypeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(PlatformTypeViewModel platformTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                var platformTypeDTO = _mapper.Map<PlatformTypeDTO>(platformTypeViewModel);

                if (!_platformTypeService.IsUniqueName(platformTypeDTO))
                {
                    ModelState.AddModelError("Name", "Platform type with such name already exist, please enter another name");
                }

                if (ModelState.IsValid)
                {
                    _platformTypeService.Update(platformTypeDTO);

                    return RedirectToAction("GetAll");
                }
            }

            return View(platformTypeViewModel);
        }
    }
}