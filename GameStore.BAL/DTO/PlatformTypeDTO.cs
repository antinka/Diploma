using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BAL.DTO
{
    public class PlatformTypeDTO
    {
        [Key]
        public Guid Id { get; set; }
        public string Type { get; set; }
    }
}
