﻿using System;
using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.Web.App_LocalResources;
using GameStore.Web.Authorization.Interfaces;
using GameStore.Web.ViewModels;
using GameStore.Web.ViewModels.Account;

namespace GameStore.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AccountController(
            IUserService userService, 
            IMapper mapper,
            IAuthentication authentication) : base(authentication)
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
        public ActionResult Register(RegisterViewModel model)
        {
            var userDto = _mapper.Map<UserDTO>(model);

            if (!_userService.IsUniqueName(userDto))
            {
                ModelState.AddModelError("Name", GlobalRes.ExistUserName);
            }

            if (ModelState.IsValid)
            {
                var userId = CurrentUser.Id;

                if (userId == Guid.Empty)
                {
                    if (HttpContext.Request.Cookies["userId"] != null)
                    {
                        userId = Guid.Parse(HttpContext.Request.Cookies["userId"].Value);
                    }
                    else
                    {
                        userId = Guid.NewGuid();
                        HttpContext.Response.Cookies["userId"].Value = userId.ToString();
                    }
                }

                string userInformation = HttpContext.Request.Cookies["userInformation"].Value;

                if (userInformation == "Girl 18+")
                {
                    userDto.Id = userId;
                    userDto.Adulthood = true;
                    userDto.IsWoman = true;
                }
                else if (userInformation == "Boy 18+")
                {
                    userDto.Id = userId;
                    userDto.Adulthood = true;
                    userDto.IsWoman = false;
                }
                else if (userInformation == "Girl less than 18")
                {
                    userDto.Id = userId;
                    userDto.Adulthood = false;
                    userDto.IsWoman = true;
                }
                else if (userInformation == "Boy less than 18")
                {
                    userDto.Id = userId;
                    userDto.Adulthood = false;
                    userDto.IsWoman = false;
                }

                _userService.AddNew(userDto);

                return RedirectToAction("Login");
            }

            return View(model);
        }

        [HttpGet]
        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isLogin = Authentication.Login(model.Name, model.Password, model.IsPersistent);

                if (isLogin)
                {
                    return RedirectToAction("FilteredGames", "Game");
                }

                ModelState.AddModelError(string.Empty, GlobalRes.ErrorLogin);

                return View(model);
            }

            return View(model);
        }

        [Authorize]
        public ActionResult LogOut()
        {
            Authentication.LogOut();

            return RedirectToAction("FilteredGames", "Game");
        }

        [Authorize]
        public ActionResult PersonalArea()
        {
            var user = _userService.GetByName(CurrentUser.Name);
            var userViewModel = _mapper.Map<UserViewModel>(user);

            return View(userViewModel);
        }
    }
}