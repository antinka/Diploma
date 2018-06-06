using System.ComponentModel.DataAnnotations;

namespace GameStore.BLL.Enums
{
    public enum SortType
    {
        [Display(Name = "Most Popular")]
        MostPopular,
        [Display(Name = "Most Commented")]
        MostCommented,
        [Display(Name = "Price Asc")]
        PriceAsc,
        [Display(Name = "Price Desc")]
        PriceDesc,
        [Display(Name = "New By Date")]
        NewByDate
    }
}
