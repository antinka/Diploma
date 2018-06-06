using System;
using System.Linq;
using System.Security.Principal;

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

        public bool IsInRole(string roles)
        {
            var userRoles = _userIdentity.User.Roles;

            if (string.IsNullOrWhiteSpace(roles))
                return false;
            var rolesArray = roles.Split(new[] { "," },
                StringSplitOptions.RemoveEmptyEntries);
            foreach (var role in rolesArray)
            {
                var hasRole = userRoles.Any(p => p.Name.Contains(role));
                if (hasRole)
                    return true;
            }
            return false;
        }
    }
}