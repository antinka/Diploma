using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BAL.Exeption
{
    class GameStoreExeption : Exception
    {
        ILog log = LogManager.GetLogger("LOGGER");
        public GameStoreExeption(string message)
            : base(message)
        {
            log.Info(message);
        }
    }
}