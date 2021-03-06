﻿using System.Collections.Generic;
using GameStore.BLL.Filters.Interfaces;

namespace GameStore.BLL.Filters.Abstract
{
    public abstract class Pipeline<T> where T : class
    {
        protected readonly List<IPipeLine<T>> Filters = new List<IPipeLine<T>>();

        public Pipeline<T> Register(IPipeLine<T> pipeLine)
        {
            Filters.Add(pipeLine);
            return this;
        }

        public abstract T Process(T input);
    }
}