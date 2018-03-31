using GameStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.Entities
{
    public class BaseEntity:IBaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public bool IsDelete { get; set; }
    }
}
