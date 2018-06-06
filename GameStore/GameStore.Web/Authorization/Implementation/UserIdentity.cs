using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Authorization.Implementation
{
    public class UserIdentity : IIdentity
    {
        private readonly User _user;

        public UserIdentity()
        {
            _user = new User
            {
                Name = "guest",
                Roles = new List<RoleViewModel> {new RoleViewModel() {Name = UserRole.Guest.ToString()}}
            };
        }

        public UserIdentity(User user)
        {
            _user = user;
        }

        public User User => _user;

        public virtual string Name => User.Name;

        public virtual string AuthenticationType => typeof(User).ToString();

        public virtual bool IsAuthenticated =>
            !User.Roles.Contains(new RoleViewModel() {Name = UserRole.Guest.ToString()});
    }
}