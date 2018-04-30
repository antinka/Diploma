using System.Collections.Generic;

namespace GameStore.DAL.Entities
{
    public class Publisher : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string HomePage { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
