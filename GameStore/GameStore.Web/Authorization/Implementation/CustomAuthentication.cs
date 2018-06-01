using System;
using System.Web;;
using System.Web.Security;
using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.Web.Authorization.Interfaces;
using GameStore.Web.ViewModels;
using log4net;

namespace GameStore.Web.Authorization.Implementation
{
    public class CustomAuthentication : IAuthentication
    {
        public HttpContext HttpContext { get; set; }

        private const string cookieName = "__AUTH_COOKIE";
        private IPrincipal _currentUser;
        private readonly ILog _log;
        private readonly IUserService _userService;

        public CustomAuthentication(ILog log, IUserService userService, IMapper mapper)
        {
            _log = log;
            _userService = userService;
        }

        public IPrincipal CurrentUser
        {
            get
            {
                if (_currentUser == null)
                {
                    try
                    {
                        HttpCookie authCookie = HttpContext.Request.Cookies.Get(cookieName);
                        if (authCookie != null && !string.IsNullOrEmpty(authCookie.Value))
                        {
                            var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                            UserDTO user = _userService.(ticket.Name);

                            var syncUser = Mapper.Map<UserModel>(user);
                            _currentUser = new UserProvider();
                        }
                        else
                        {
                            _currentUser = new UserProvider();
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.Info("Failed authentication: " + ex.Message);
                        _currentUser = new UserProvider();
                    }
                }
                return _currentUser;
            }
        }

        public UserViewModel Login(string login, string password, bool isPersistent)
        {
            var retUser = Repository.Login(login, password);
            if (retUser != null)
            {
                CreateCookie(login, isPersistent);
            }
            return retUser;
        }

        public void LogOut()
        {
            var httpCookie = HttpContext.Response.Cookies[FormsAuthentication.FormsCookieName];

            if (httpCookie != null)
            {
                httpCookie.Value = string.Empty;
            }

            HttpContext.Request.Cookies.Remove(FormsAuthentication.FormsCookieName);
        }

        private void CreateCookie(string userName, bool isPersistent = false)
        {
            var ticket = new FormsAuthenticationTicket(
                1,
                userName,
                DateTime.UtcNow,
                DateTime.UtcNow.Add(FormsAuthentication.Timeout),
                isPersistent,
                string.Empty,
                FormsAuthentication.FormsCookiePath);
            var encTicket = FormsAuthentication.Encrypt(ticket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName)
            {         
                Value = encTicket,
                Expires = DateTime.Now.Add(FormsAuthentication.Timeout)
            };

            HttpContext.Response.Cookies.Set(authCookie);
        }
    }
}