using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.DAL.Entities
{
    public class PlatformType: BaseEntity
    {
        [Index("PlatformType_Index_Type", 1, IsUnique = true)]
        [MaxLength(450)]
        public string Type { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
