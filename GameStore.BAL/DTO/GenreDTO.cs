using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BAL.DTO
{
    public class GenreDTO
    {
        [Key]
        public Guid Id { get; set; }
        public Guid? IdParentGanre { get; set; }
        public string Name { get; set; }
        public ICollection<GameDTO> Games { get; set; }

        public GenreDTO(string Name)
        {
            Id = Guid.NewGuid();
            this.Name = Name;
        }

        public GenreDTO(string Name, Guid IdParentGanre)
        {
            Id = Guid.NewGuid();
            this.Name = Name;
            this.IdParentGanre = IdParentGanre;
        }
    }
}
