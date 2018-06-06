using System.ComponentModel.DataAnnotations;

namespace GameStore.BLL.Enums
{
    public enum PageSize
    {
        [Display(Name = "10")]
        Ten,
        [Display(Name = "20")]
        Twenty,
        [Display(Name = "50")]
        Fifty,
        [Display(Name = "100")]
        OneHundred,
        All
    }
}
