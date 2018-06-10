using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.Web.App_LocalResources;
using GameStore.Web.Authorization.Interfaces;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IRoleService roleService, IMapper mapper, IAuthentication authentication) : base(authentication)
        {
            _userService = userService;
            _roleService = roleService;
            _mapper = mapper;
        }

        public ActionResult GetAll()
        {
            var users = _mapper.Map<IEnumerable<UserViewModel>>(_userService.GetAll());

            return View(users);
        }

        [HttpGet]
        public ActionResult New()
        {
            return View(GetUserViewModelForCreate(new UserViewModel()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(UserViewModel userViewModel)
        {
            if (userViewModel.SelectedRolesName == null)
            {
                ModelState.AddModelError("Roles", GlobalRes.ChooseRoles);
            }

            if (ModelState.IsValid)
            {
                var userDTO = _mapper.Map<UserDTO>(userViewModel);

                var isUniqueName = _userService.IsUniqueName(userDTO);

                if (isUniqueName)
                {
                    _userService.AddNew(userDTO);

                    return RedirectToAction("GetAll");
                }

                ModelState.AddModelError("Name", GlobalRes.ExistPublisherName);
            }

            return View(GetUserViewModelForCreate(userViewModel));
        }

        [HttpGet]
        public ActionResult Get(string name)
        {
            var userDTO = _userService.GetByName(name);
            var userViewModel = _mapper.Map<UserViewModel>(userDTO);

            return View(userViewModel);
        }

        [HttpPost]
        public ActionResult Remove(Guid userId)
        {
            _userService.Delete(userId);

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public ActionResult Update(string name)
        {
            var userDTO = _userService.GetByName(name);
            var userViewModel = _mapper.Map<UserViewModel>(userDTO);

            return View(GetGameViewModelForUpdate(userViewModel));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UserViewModel userViewModel)
        {
            if (userViewModel.SelectedRolesName == null)
            {
                ModelState.AddModelError("Roles", GlobalRes.ChooseRoles);
            }

            if (ModelState.IsValid)
            {
                var userDTO = _mapper.Map<UserDTO>(userViewModel);

                var isUniqueName = _userService.IsUniqueName(userDTO);

                if (isUniqueName)
                {
                    _userService.Update(userDTO);

                    return RedirectToAction("GetAll");
                }

                ModelState.AddModelError("Name", GlobalRes.ExistPublisherName);
            }

            return View(GetGameViewModelForUpdate(userViewModel));
        }

        private UserViewModel CreateCheckBoxForUserViewModel(UserViewModel gameViewModel)
        {
            var rolelist = _mapper.Map<IEnumerable<RoleViewModel>>(_roleService.GetAll());
 
            var listPlatformBoxes = new List<CheckBox>();
            rolelist.Select(platform => { listPlatformBoxes.Add(new CheckBox() { Text = platform.Name }); return platform; }).ToList();
            gameViewModel.ListRoles = listPlatformBoxes;

            return gameViewModel;
        }

        private UserViewModel GetUserViewModelForCreate(UserViewModel gameViewModel)
        {
            gameViewModel = CreateCheckBoxForUserViewModel(gameViewModel);

            if (gameViewModel.SelectedRolesName != null)
            {
                gameViewModel.SelectedRoles = gameViewModel.ListRoles.Where(x => gameViewModel.SelectedRolesName.Contains(x.Text));
            }

            return gameViewModel;
        }

        private UserViewModel GetGameViewModelForUpdate(UserViewModel gameViewModel)
        {
            gameViewModel = CreateCheckBoxForUserViewModel(gameViewModel);
            gameViewModel = GetUserViewModelForCreate(gameViewModel);

            if (gameViewModel.Roles != null)
            {
                gameViewModel.SelectedRoles = gameViewModel.ListRoles.Where(x => gameViewModel.Roles.Any(g => g.Name.Contains(x.Text)));
            }

            return gameViewModel;
        }
    }
}