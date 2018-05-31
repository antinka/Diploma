using System.Collections.Generic;
using GameStore.BLL.Filters.GameFilters.Interfaces;

namespace GameStore.BLL.Filters.GameFilters.Abstract
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