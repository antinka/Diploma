using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Models
{
    public class GenreViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid? IdParentGanre { get; set; }
        [Index("Index_Name", 1, IsUnique = true)]
        public string Name { get; set; }
       
    }
}
