using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BAL.DTO
{
    public class GameDTO
    {
        [Key]
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<CommentDTO> Comments { get; set; }
        public ICollection<GenreDTO> Genres { get; set; }
        public ICollection<PlatformTypeDTO> PlatformTypes { get; set; }

        public GameDTO()
        { }

        public GameDTO(string Name, string Description)
        {
            Id = Guid.NewGuid();
            Key = Id.ToString();
            this.Name = Name;
            this.Description = Description;
        }

        public GameDTO(string Name, string Description, string Key)
        {
            Id = Guid.NewGuid();
            this.Key = Key;
            this.Name = Name;
            this.Description = Description;
        }
    }
}
