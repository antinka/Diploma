using GameStore.BLL.Filtration.Interfaces;
using System.Collections.Generic;

namespace GameStore.BLL.Filtration.Implementation
{
    public abstract class Pipeline<T> where T : class
    {
        protected readonly List<IPipeLine<T>> filters = new List<IPipeLine<T>>();

        public Pipeline<T> Register(IPipeLine<T> pipeLine)
        {
            filters.Add(pipeLine);
            return this;
        }

        public abstract T Process(T input);
    }
}