using System.Collections.Generic;
using System.Linq;
using GameStore.Web.Authorization.Interfaces;

namespace GameStore.Web.Authorization.Implementation
{
    public class UserIdentity : IIdentity
    {
        private readonly User _user;

        public UserIdentity()
        {
            _user = new User { Name = "guest", Roles = new List<UserRole> { UserRole.Guest} };
        }

        public UserIdentity(User user)
        {
            _user = user;
        }

        public User User
        {
            get { return _user; }
        }

        public virtual string Name
        {
            get { return User.Name; }
        }

        public virtual string AuthenticationType
        {
            get { return typeof(User).ToString(); }
        }

        public virtual bool IsAuthenticated
        {
            get { return !User.Roles.Contains(UserRole.Guest); }
        }
    }
}