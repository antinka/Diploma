using System;
using System.Collections.Generic;

namespace GameStore.Web.ViewModels
{
    public class RoleViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<UserViewModel> Users { get; set; }
    }
}