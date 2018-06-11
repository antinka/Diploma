using System;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.Web.Authorization.Interfaces;
using log4net;

namespace GameStore.Web.Authorization.Implementation
{
    public class CustomAuthentication : IAuthentication
    {
        private const string cookieName = "__AUTH_COOKIE"; 
        private readonly ILog _log;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private IPrincipal _currentUser;
       
        public CustomAuthentication(ILog log, IUserService userService, IMapper mapper)
        {
            _log = log;
            _mapper = mapper;
            _userService = userService;
        }

        public HttpContext HttpContext { get; set; }

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

        public bool Login(string login, string password, bool isPersistent)
        {
            var userDTO = _userService.Login(login, password);

            if (userDTO != null)
            {
                CreateCookie(login, isPersistent);

                return true;
            }

            return false;
        }

        public void LogOut()
        {
            var httpCookie = HttpContext.Response.Cookies.Get(cookieName);

            if (httpCookie != null)
            {
                httpCookie.Value = string.Empty;
            }

            HttpContext.Request.Cookies.Remove(cookieName);
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
            var authCookie = new HttpCookie(cookieName)
            {         
                Value = encTicket,
                Expires = DateTime.Now.Add(FormsAuthentication.Timeout)
            };

            HttpContext.Response.Cookies.Set(authCookie);
        }
    }
}