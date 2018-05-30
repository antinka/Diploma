using System.ComponentModel.DataAnnotations;
using GameStore.Web.App_LocalResources;

namespace GameStore.Web.ViewModels
{
    public class ShipperViewModel
    {
        public string Id { get; set; }

        [Display(Name = "CompanyName", ResourceType = typeof(GlobalRes))]
        public string CompanyName { get; set; }

        [Display(Name = "Phone", ResourceType = typeof(GlobalRes))]
        public string Phone { get; set; }
    }
}