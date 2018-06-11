using System;
using System.ComponentModel.DataAnnotations;
using GameStore.DAL.Interfaces;

namespace GameStore.DAL.Entities
{
    public class BaseEntity : IBaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public bool IsDelete { get; set; }
    }
}
