using System;

namespace GameStore.DAL.Entities
{
    public class Log
    {
        public DateTime DateTime { get; set; }

        public string Action { get; set; }

        public string EntityType { get; set; }

        public string OldObject { get; set; }

        public string NewObject { get; set; }
    }
}
