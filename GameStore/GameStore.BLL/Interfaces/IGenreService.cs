using System;
using System.Collections.Generic;
using GameStore.BLL.DTO;

namespace GameStore.BLL.Interfaces
{
    public interface IGenreService
    {
        GenreDTO GetById(Guid id);

        IEnumerable<GenreDTO> GetAll();

        void AddNew(ExtendGenreDTO genreDto);

        void Update(ExtendGenreDTO genreDto);

        void Delete(Guid id);

        ExtendGenreDTO GetByName(string name);

        bool IsPossibleRelation(ExtendGenreDTO genreDto);

        bool IsUniqueEnName(ExtendGenreDTO genreDto);

        bool IsUniqueRuName(ExtendGenreDTO genreDto);
    }
}
