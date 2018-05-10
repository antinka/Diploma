using System.ComponentModel.DataAnnotations;
using GameStore.BLL.Resources;

namespace GameStore.BLL.Enums
{
    public enum SortDate
    {
        [Display(Name = "week", ResourceType = typeof(EnumRes))]
        week,
        [Display(Name = "month", ResourceType = typeof(EnumRes))]
        month,
        [Display(Name = "oneYear", ResourceType = typeof(EnumRes))]
        oneYear,
        [Display(Name = "twoYear", ResourceType = typeof(EnumRes))]
        twoYear,
        [Display(Name = "threeYear", ResourceType = typeof(EnumRes))]
        threeYear
    }
}
