using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BAL.Exeption
{
    class GameStoreExeption : Exception,ISerializable
    {
        readonly ILog _log = LogManager.GetLogger("LOGGER");
        public GameStoreExeption(string message)
            : base(message)
        {
            _log.Info(message);
        }
    }
}