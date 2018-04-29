using System;

namespace GameStore.BLL.Exeption
{
    public class NotUniqueParameter : Exception
    {
        public NotUniqueParameter(string message)
            : base(message)
        {
        }
    }
}
