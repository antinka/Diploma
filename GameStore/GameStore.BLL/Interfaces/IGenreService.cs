using System;
using System.Collections.Generic;
using GameStore.BLL.DTO;

namespace GameStore.BLL.Interfaces
{
    public interface IGenreService
    {
        GenreDTO GetById(Guid id);

        IEnumerable<GenreDTO> GetAll();

        bool AddNew(GenreDTO genreDto);

        bool Update(GenreDTO genreDto);

        void Delete(Guid id);

        GenreDTO GetByName(string name);

        bool IsPossibleRelation(GenreDTO genreDto);
    }
}
