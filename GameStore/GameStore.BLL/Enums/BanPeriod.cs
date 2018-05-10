using System.ComponentModel.DataAnnotations;
using GameStore.BLL.Resources;

namespace GameStore.BLL.Enums
{
    public enum BanPeriod
    {
        [Display(Name = "Hour", ResourceType = typeof(EnumRes))]
        Hour,

        Day,
        Week,
        Month,
        Permanent
    }
}
