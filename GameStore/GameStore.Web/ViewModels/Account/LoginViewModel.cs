using GameStore.Web.App_LocalResources;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.ViewModels.Account
{
    public class LoginViewModel
    {
        [StringLength(40, MinimumLength = 3, ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "UserNameExpression3_40")]
        [Display(ResourceType = typeof(GlobalRes), Name = "UserName")]
        public string Name { get; set; }

        [StringLength(40, MinimumLength = 3, ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "PasswordExpression3_40")]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(GlobalRes), Name = "Password")]
        public string Password { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "IsPersistent")]
        public bool IsPersistent { get; set; }
    }
}