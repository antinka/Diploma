using System;

namespace GameStore.BLL.Exeption
{
    public class EntityNotFound : Exception
    {
        public EntityNotFound(string message)
            : base(message)
        {
        }
    }
}