using System;
using System.Collections.Generic;
using GameStore.BLL.DTO;

namespace GameStore.BLL.Interfaces
{
    public interface IGenreService
    {
        GenreDTO GetById(Guid id);

        IEnumerable<GenreDTO> GetAll();
    }
}
