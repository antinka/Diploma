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
        private readonly IPublisherService _publisherService;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public UserController(
            IUserService userService,
            IPublisherService publisherService,
            IRoleService roleService,
            IMapper mapper,
            IAuthentication authentication) : base(authentication)
        {
            _userService = userService;
            _publisherService = publisherService;
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
            var publishers = _mapper.Map<IEnumerable<DetailsPublisherViewModel>>(_publisherService.GetAll());
            var userViewModel = new UserViewModel()
            {
                PublisherList = new SelectList(publishers, "Id", "Name")
            };

            return View(GetUserViewModelForCreate(userViewModel));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(UserViewModel userViewModel)
        {
            if (userViewModel.SelectedRolesName == null)
            {
                ModelState.AddModelError("Roles", GlobalRes.ChooseRoles);
            }

            if (userViewModel.StartDateBaned > userViewModel.EndDateBaned)
            {
                ModelState.AddModelError("StartDateBaned", GlobalRes.DataTimeFromTo);
            }

            if (userViewModel.PublisherId != null
                && userViewModel.SelectedRolesName != null
                && !userViewModel.SelectedRolesName.Contains("Publisher"))
            {
                ModelState.AddModelError("Publisher", GlobalRes.ChoosePablisherRole);
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

            return View(GetUserViewModelForUpdate(userViewModel));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UserViewModel userViewModel)
        {
            if (userViewModel.SelectedRolesName == null)
            {
                ModelState.AddModelError("Roles", GlobalRes.ChooseRoles);
            }

            if (userViewModel.StartDateBaned > userViewModel.EndDateBaned)
            {
                ModelState.AddModelError("StartDateBaned", GlobalRes.DataTimeFromTo);
            }

            if (userViewModel.PublisherId != null
                && userViewModel.SelectedRolesName != null
                && !userViewModel.SelectedRolesName.Contains("Publisher"))
            {
                ModelState.AddModelError("Publisher", GlobalRes.ChoosePablisherRole);
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

            return View(GetUserViewModelForUpdate(userViewModel));
        }

        private UserViewModel CreateCheckBoxForUserViewModel(UserViewModel userViewModel)
        {
            var rolelist = _mapper.Map<IEnumerable<RoleViewModel>>(_roleService.GetAll());

            var listRoles = new List<CheckBox>();
            rolelist.Select(role => { listRoles.Add(new CheckBox() { Text = role.Name }); return role; }).ToList();
            userViewModel.ListRoles = listRoles;

            return userViewModel;
        }

        private UserViewModel GetUserViewModelForCreate(UserViewModel userViewModel)
        {
            userViewModel = CreateCheckBoxForUserViewModel(userViewModel);

            if (userViewModel.SelectedRolesName != null)
            {
                userViewModel.SelectedRoles = userViewModel.ListRoles
                    .Where(x => userViewModel.SelectedRolesName.Contains(x.Text));
            }

            var publishers = _mapper.Map<IEnumerable<DetailsPublisherViewModel>>(_publisherService.GetAll());
            userViewModel.PublisherList = new SelectList(publishers, "Id", "Name");

            return userViewModel;
        }

        private UserViewModel GetUserViewModelForUpdate(UserViewModel gameViewModel)
        {
            gameViewModel = CreateCheckBoxForUserViewModel(gameViewModel);
            gameViewModel = GetUserViewModelForCreate(gameViewModel);

            if (gameViewModel.Roles != null)
            {
                gameViewModel.SelectedRoles = gameViewModel.ListRoles
                    .Where(x => gameViewModel.Roles.Any(g => g.Name.Contains(x.Text)));
            }

            return gameViewModel;
        }
    }
}