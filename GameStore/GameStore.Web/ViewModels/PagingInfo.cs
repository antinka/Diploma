using System;

namespace GameStore.Web.ViewModels
{
    public class PagingInfo
    {
        public int TotalItemsByFilter { get; set; }

        public int ItemsPerPage { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages
        {
            get
            {
                if (ItemsPerPage > 0)
                {
                    return (int)Math.Ceiling((decimal)TotalItemsByFilter / ItemsPerPage);
                }

                return 1;
            }
        }
    }
}