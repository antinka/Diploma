using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.Web.Authorization.Interfaces;
using log4net;
using System;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace GameStore.Web.Authorization.Implementation
{
    public class CustomAuthentication : IAuthentication
    {
        public HttpContext HttpContext { get; set; }

        private const string cookieName = "__AUTH_COOKIE";
        private IPrincipal _currentUser;
        private readonly ILog _log;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public CustomAuthentication(ILog log, IUserService userService, IMapper mapper)
        {
            _log = log;
            _mapper = mapper;
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
                            var userDTO = _userService.GetByName(ticket.Name);

                            var user = _mapper.Map<User>(userDTO);
                            _currentUser = new UserProvider(user);
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

        public User Login(string login, string password, bool isPersistent)
        {
            var userDTO = _userService.Login(login, password);
            var user = _mapper.Map<User>(userDTO);

            if (user != null)
            {
                CreateCookie(login, isPersistent);
            }

            return user;
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