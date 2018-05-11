using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Exeption;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.BLL.Service
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILog _log;

        public GenreService(IUnitOfWork uow, IMapper mapper, ILog log)
        {
            _unitOfWork = uow;
            _mapper = mapper;
            _log = log;
        }

        public GenreDTO GetById(Guid id)
        {
            var genre = _unitOfWork.Genres.GetById(id);

            if (genre == null)
            {
                throw new EntityNotFound($"{nameof(GenreService)} - genre with such id {id} did not exist");
            }

            return _mapper.Map<GenreDTO>(genre);
        }

        public IEnumerable<GenreDTO> GetAll()
        {
            var genres = _unitOfWork.Genres.GetAll();
            var genresDto = _mapper.Map<IEnumerable<GenreDTO>>(genres);

            foreach (var genre in genresDto)
            {
                foreach (var parentGenre in genresDto)
                {
                    if (genre.ParentGenreId == parentGenre.Id)
                    {
                        genre.ParentGenreName = parentGenre.Name;
                    }
                }
            }

            return genresDto;
        }

        public bool AddNew(GenreDTO genreDto)
        {
            var genre = _unitOfWork.Genres.Get(g => g.Name == genreDto.Name).FirstOrDefault();

            if (genre == null)
            {
                genreDto.Id = Guid.NewGuid();
                _unitOfWork.Genres.Create(_mapper.Map<Genre>(genreDto));
                _unitOfWork.Save();

                _log.Info($"{nameof(GenreService)} - add new genre { genreDto.Id}");

                return true;
            }
            else
            {
                _log.Info($"{nameof(GenreService)} - attempt to add new genre with not unique name");

                return false;
            }
        }

        public bool Update(GenreDTO genreDto)
        {
            if (TakeGenreById(genreDto.Id) != null)
            {
                var genreFromDb = _unitOfWork.Genres.Get(x => x.Name == genreDto.Name).FirstOrDefault();

                if (genreFromDb == null)
                {
                    var genre = _mapper.Map<Genre>(genreDto);

                    _unitOfWork.Genres.Update(genre);
                    _unitOfWork.Save();

                    _log.Info($"{nameof(GenreService)} - update genre {genreDto.Id}");

                    return true;
                }

                return false;
            }
            else
            {
                _log.Info($"{nameof(GenreService)} - attempt to update genre with exist genre name");

                return false;
            }

        }

        public void Delete(Guid id)
        {
            TakeGenreById(id);

            _unitOfWork.Genres.Delete(id);
            _unitOfWork.Save();

            _log.Info($"{nameof(GenreService)} - delete publisher {id}");
        }

        public GenreDTO GetByName(string name)
        {
            var genre = _unitOfWork.Genres.Get(g => g.Name == name).FirstOrDefault();

            if (genre == null)
            {
                throw new EntityNotFound($"{nameof(GenreService)} - genre with such name {name} did not exist");
            }

            var genresDto = _mapper.Map<GenreDTO>(genre);

            if (genre.ParentGenreId != null)
                genresDto.ParentGenreName = _unitOfWork.Genres.GetById(genre.ParentGenreId.Value).Name;

            return genresDto;
        }

        public bool IsPossibleRelation(GenreDTO genreDto)
        {
            var reverseGenreDto = _unitOfWork.Genres.Get(g => g.Id == genreDto.ParentGenreId && g.ParentGenreId == genreDto.Id).FirstOrDefault();

            if (reverseGenreDto == null)
                return true;

            var genre = _unitOfWork.Genres.Get(g => g.Name == genreDto.Name).FirstOrDefault();

            if (genre == null)
                return true;

            return false;
        }

        private Genre TakeGenreById(Guid id)
        {
            var genre = _unitOfWork.Genres.GetById(id);

            if (genre == null)
                throw new EntityNotFound($"{nameof(GenreService)} - genre with such id {id} did not exist");

            return genre;
        }
    }
}
