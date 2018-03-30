using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.Entities
{
    public class Genre: BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public Guid? IdParentGanre { get; set; }
        [Index("Index_Name", 1, IsUnique = true)]
        [MaxLength(450)]
        public string Name { get; set; }

        public virtual IEnumerable<Game> Games { get; set; }
    }
}
