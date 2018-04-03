using log4net;
using System;

namespace GameStore.BAL.Exeption
{
    class EntityNotFound : Exception
    {
        public readonly ILog _log = LogManager.GetLogger("LOGGER");

        public EntityNotFound(string message)
            : base(message)
        {
            _log.Info(message);
        }
    }
}