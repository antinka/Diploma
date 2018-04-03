using System;
using System.ComponentModel.DataAnnotations;

namespace GameStore.BAL.DTO
{
    public class PlatformTypeDTO
    {
        public Guid Id { get; set; }

        public string Type { get; set; }
    }
}
