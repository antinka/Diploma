using log4net;
using System;

namespace GameStore.BAL.Exeption
{
    class EntityNotFound : Exception
    {
        private readonly ILog _log;

        public EntityNotFound(string message, ILog log)
            : base(message)
        {
            _log.Info(message);
            _log=log;
        }
    }
}