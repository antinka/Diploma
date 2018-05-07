using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.Filters;
using GameStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GameStore.Controllers
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
        public ActionResult New(PlatformTypeViewModel platformTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                var platformTypeDTO = _mapper.Map<PlatformTypeDTO>(platformTypeViewModel);
                var isAddNewPlatformType = _platformTypeService.AddNew(platformTypeDTO);

                if (isAddNewPlatformType)
                    return RedirectToAction("Get", new { platformTypeName = platformTypeViewModel.Name });

                ModelState.AddModelError("Name", "Not Unique Parameter");
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
        public ActionResult Update(PlatformTypeViewModel platformTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                var platformTypeDTO = _mapper.Map<PlatformTypeDTO>(platformTypeViewModel);
                _platformTypeService.Update(platformTypeDTO);

                return RedirectToAction("GetAll");
            }

            return View(platformTypeViewModel);
        }
    }
}