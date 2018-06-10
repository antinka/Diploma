using System;
using System.ComponentModel.DataAnnotations;

namespace GameStore.BLL.Enums
{
    public enum PageSize
    {
        [Display(Name = "10")]
        Ten = 10,
        [Display(Name = "20")]
        Twenty = 20,
        [Display(Name = "50")]
        Fifty = 50,
        [Display(Name = "100")]
        OneHundred = 100,
        All = Int32.MaxValue
    }
}
