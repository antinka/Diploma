using System;
using System.Collections.Generic;

namespace GameStore.Web.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsBaned { get; set; }

        public DateTime? StartDateBaned { get; set; }

        public DateTime? EndDateBaned { get; set; }

        public ICollection<RoleViewModel> Roles { get; set; }
    }
}