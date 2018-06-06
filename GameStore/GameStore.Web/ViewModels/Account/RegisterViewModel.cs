using GameStore.Web.App_LocalResources;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.ViewModels.Account
{
    public class RegisterViewModel
    {
        [StringLength(40, MinimumLength = 3, ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "UserNameExpression3_40")]
        [Display(ResourceType = typeof(GlobalRes), Name = "UserName")]
        public string Name { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "FirstName")]
        public string FirstName { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "LastName")]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(40, MinimumLength = 3, ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "PasswordExpression3_40")]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(GlobalRes), Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "NameExpression3_40")]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(GlobalRes), Name = "ConfirmePassword")]
        [Compare("Password", ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "ConfirmePasswordError")]
        public string ConfirmPassword { get; set; }
    }
}