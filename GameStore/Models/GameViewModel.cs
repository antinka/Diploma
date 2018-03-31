using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Models
{
    public class GameViewModel
    {
       [Key]
       public Guid Id { get; set; }
       [Index("Index_Key", 1, IsUnique = true)]
       public string Key { get; set; }
	   public string Name { get; set; }
       public string Description { get; set; }

    }
}
