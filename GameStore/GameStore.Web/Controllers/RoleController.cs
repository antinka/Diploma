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
    [Authorize(Roles = "Administrator")]
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public RoleController(
            IRoleService roleService,
            IMapper mapper,
            IAuthentication authentication) : base(authentication)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        public ActionResult GetAll()
        {
            var roles = _mapper.Map<IEnumerable<RoleViewModel>>(_roleService.GetAll());

            return View(roles);
        }

        [HttpGet]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(RoleViewModel role)
        {
            if (ModelState.IsValid)
            {
                var roleDTO = _mapper.Map<RoleDTO>(role);

                var isUniqueName = _roleService.IsUniqueName(roleDTO);

                if (isUniqueName)
                {
                    _roleService.AddNew(roleDTO);

                    return RedirectToAction("GetAll");
                }

                ModelState.AddModelError("Name", GlobalRes.ExistPublisherName);
            }

            return View(role);
        }

        [HttpGet]
        public ActionResult Get(string name)
        {
            var roleDTO = _roleService.GetByName(name);
            var roleViewModel = _mapper.Map<RoleViewModel>(roleDTO);

            return View(roleViewModel);
        }

        [HttpPost]
        public ActionResult Remove(Guid roleId)
        {
            _roleService.Delete(roleId);

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public ActionResult Update(string name)
        {
            var roleDTO = _roleService.GetByName(name);
            var roleViewModel = _mapper.Map<RoleViewModel>(roleDTO);

            return View(roleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                var roleDTO = _mapper.Map<RoleDTO>(roleViewModel);

                var isUniqueName = _roleService.IsUniqueName(roleDTO);

                if (isUniqueName)
                {
                    _roleService.Update(roleDTO);

                    return RedirectToAction("GetAll");
                }

                ModelState.AddModelError("Name", GlobalRes.ExistPublisherName);
            }

            return View(roleViewModel);
        }
    }
}