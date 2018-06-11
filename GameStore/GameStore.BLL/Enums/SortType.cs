using System.ComponentModel.DataAnnotations;
using GameStore.BLL.Resources;

namespace GameStore.BLL.Enums
{
    public enum SortType
    {
        [Display(Name = "MostPopular", ResourceType = typeof(EnumRes))]
        MostPopular,
        [Display(Name = "MostCommented", ResourceType = typeof(EnumRes))]
        MostCommented,
        [Display(Name = "PriceAsc", ResourceType = typeof(EnumRes))]
        PriceAsc,
        [Display(Name = "PriceDesc", ResourceType = typeof(EnumRes))]
        PriceDesc,
        [Display(Name = "NewByDate", ResourceType = typeof(EnumRes))]
        NewByDate
    }
}
