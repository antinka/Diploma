using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.Web.App_LocalResources;
using GameStore.Web.Authorization.Interfaces;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Controllers
{
    public class AccountController : BaseController
    {

        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AccountController(IUserService userService, IMapper mapper, IAuthentication authentication) : base(authentication)
        {
            _mapper = mapper;
            _userService = userService;
        }


        [HttpGet]
        public ViewResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserViewModel model)
        {
            var userDto = Mapper.Map<UserDTO>(model);

            if (!ModelState.IsValid)
            {
                if (!_userService.IsUniqueName(userDto))
                {
                    ModelState.AddModelError("Name", "exist name");
                }

                return View(model);
            }

            _userService.AddNew(userDto);

            return RedirectToAction("Login");
        }

        [HttpGet]
        public ViewResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(UserViewModel model)
        {

            if (ModelState.IsValid)
            {
                bool isLogin = Authentication.Login(model.Name, model.Password, model.RememberMe);

                if (isLogin)
                {
                    return RedirectToAction("FilteredGames", "Game");
                }



                ModelState.AddModelError("", "");

                return View(model);
            }
        }
    }
}