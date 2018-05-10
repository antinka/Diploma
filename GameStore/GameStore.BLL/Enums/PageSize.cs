using System.ComponentModel.DataAnnotations;
using GameStore.BLL.Resources;

namespace GameStore.BLL.Enums
{
    public enum PageSize
    {
        [Display(Name = "Ten", ResourceType = typeof(EnumRes))]
        Ten = 10,
        [Display(Name = "Twenty", ResourceType = typeof(EnumRes))]
        Twenty = 20,
        [Display(Name = "Fifty", ResourceType = typeof(EnumRes))]
        Fifty = 50,
        [Display(Name = "OneHundred", ResourceType = typeof(EnumRes))]
        OneHundred = 100,
        [Display(Name = "All", ResourceType = typeof(EnumRes))]
        All = 0
    }
}
