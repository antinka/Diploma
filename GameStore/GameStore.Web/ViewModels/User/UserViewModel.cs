using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using GameStore.Web.App_LocalResources;

namespace GameStore.Web.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "Existent")]
        [StringLength(40, MinimumLength = 3, ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "UserNameExpression3_40")]
        [Display(ResourceType = typeof(GlobalRes), Name = "UserName")]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "Existent")]
        [StringLength(40, MinimumLength = 3, ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "PasswordExpression3_40")]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(GlobalRes), Name = "Password")]
        public string Password { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "FirstName")]
        public string FirstName { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "LastName")]
        public string LastName { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "IsBaned")]
        public bool IsBaned { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "StartDateBaned")]
        public DateTime? StartDateBaned { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "EndDateBaned")]
        public DateTime? EndDateBaned { get; set; }

        public Guid? PublisherId { get; set; }

        [Display(Name = "Publisher", ResourceType = typeof(GlobalRes))]
        public DetailsPublisherViewModel Publisher { get; set; }

        public ICollection<RoleViewModel> Roles { get; set; }

        public IEnumerable<CheckBox> ListRoles { get; set; }

        public IEnumerable<CheckBox> SelectedRoles { get; set; }

        public ICollection<string> SelectedRolesName { get; set; }

        public SelectList PublisherList { get; set; }
    }
}