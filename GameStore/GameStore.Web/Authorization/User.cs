using System.Collections.Generic;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Authorization
{
    public class User
    {
        public string Name { get; set; }

        public IEnumerable<RoleViewModel> Roles { get; set; }

        public bool IsBanned { get; set; }
    }
}