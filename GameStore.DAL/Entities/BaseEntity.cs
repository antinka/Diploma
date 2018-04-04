using System;
using GameStore.DAL.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace GameStore.DAL.Entities
{
    public class BaseEntity : IBaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public bool IsDelete { get; set; }
    }
}
