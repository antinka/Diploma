﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.Interfaces
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }
        bool IsDelete { get; set; }
    }
}
