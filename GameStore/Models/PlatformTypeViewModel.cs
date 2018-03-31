using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Models
{
    public class PlatformTypeViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Index("Index_Type", 1, IsUnique = true)]
        public string Type { get; set; }

    }
}
