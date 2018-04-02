using System;

namespace GameStore.BAL.DTO
{
    public class GameDTO
    {
        public Guid Id { get; set; }

        public string Key { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
