using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Authorization.Implementation
{
    public class UserIdentity : IIdentity
    {
        public UserIdentity()
        {
            User = new User
            {
                Name = "Guest",
                Roles = new List<RoleViewModel> { new RoleViewModel() { Name = "Guest" } }
            };
        }

        public UserIdentity(User user)
        {
            User = user;
        }

        public User User { get; }

        public virtual string Name => User.Name;

        public virtual string AuthenticationType => typeof(User).ToString();

        public virtual bool IsAuthenticated
        {
            get
            {
                return User.Roles.All(r => r.Name != "Guest");
            }
        }
    }
}