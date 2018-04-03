using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.DAL.Entities
{
    public class Genre: BaseEntity
    {
        public Guid? IdParentGanre { get; set; }

        [Index("Genre_Index_Name", 1, IsUnique = true)]
        [MaxLength(450)]
        public string Name { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
