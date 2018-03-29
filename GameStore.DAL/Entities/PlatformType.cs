using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.Entities
{
    public class PlatformType
    {
        [Key]
        public Guid Id { get; set; }
        [Index("Index_Type", 1, IsUnique = true)]
        public string Type { get; set; }
        public ICollection<Game> Games { get; set; }

        public PlatformType(string Type)
        {
            Id = Guid.NewGuid();
            this.Type = Type;
        }
    }
}
