﻿using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.Web.Authorization.Interfaces;

namespace GameStore.Web.Authorization.Implementation
{
    public class UserProvider : IPrincipal
    {
        private readonly UserIdentity _userIdentity;

        public UserProvider(User user)
        {
            _userIdentity = new UserIdentity(user);
        }

        public UserProvider()
        {
            _userIdentity = new UserIdentity();
        }

        public IIdentity Identity => _userIdentity;

        public bool IsInRole(string role)
        {
            var roles = _userIdentity.User.Roles;

            return roles.Any(r => String.Equals(r.ToString(), role));
        }
    }
}