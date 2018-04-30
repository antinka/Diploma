using System;

namespace GameStore.DAL.Interfaces
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }

        bool IsDelete { get; set; }
    }
}
