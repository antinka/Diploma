using System;

namespace GameStore.BLL.CustomExeption
{
    public class EntityNotFound : Exception
    {
        public EntityNotFound(string message)
            : base(message)
        {
        }
    }
}