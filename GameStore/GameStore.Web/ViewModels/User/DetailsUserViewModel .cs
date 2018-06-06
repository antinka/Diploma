using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.Web.App_LocalResources;

namespace GameStore.Web.ViewModels
{
    public class DetailsUserViewModel
    {
        public Guid Id { get; set; }

        [StringLength(40, MinimumLength = 3, ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "UserNameExpression3_40")]
        [Display(ResourceType = typeof(GlobalRes), Name = "UserName")]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(40, MinimumLength = 3, ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "PasswordExpression3_40")]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(GlobalRes), Name = "Password")]
        public string Password { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "FirstName")]
        public string FirstName { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "LastName")]
        public string LastName { get; set; }

        public bool IsBaned { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "StartDateBaned")]
        public DateTime? StartDateBaned { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "EndDateBaned")]
        public DateTime? EndDateBaned { get; set; }

        public ICollection<RoleViewModel> Roles { get; set; }
    }
}