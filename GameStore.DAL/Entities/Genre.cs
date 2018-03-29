using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.Entities
{
    public class Genre
    {
        [Key]
        public Guid Id { get; set; }
        public Guid? IdParentGanre { get; set; }

        [Index("Index_Name", 1, IsUnique = true)]
        public string Name { get; set; }
        public ICollection<Game> Games { get; set; }

        public Genre(string Name)
        {
            Id = Guid.NewGuid();
            this.Name = Name;
        }

        public Genre(string Name, Guid IdParentGanre)
        {
            Id = Guid.NewGuid();
            this.Name = Name;
            this.IdParentGanre = IdParentGanre;
        }
    }
}
