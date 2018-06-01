using System.Collections.Generic;

namespace GameStore.Web.Authorization
{
    public class User
    {
        public string Name { get; set; }

        public IEnumerable<UserRole> Roles { get; set; }

        public bool IsBanned { get; set; }
    }
}