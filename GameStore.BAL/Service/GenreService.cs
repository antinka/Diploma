using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Exeption;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.DAL.Entities;
using log4net;

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

        public void AddNew(GenreDTO genreDto)
        {
            var genre = _unitOfWork.Genres.Get(x => x.Name == genreDto.Name).FirstOrDefault();

            if (genre == null)
            {
                genreDto.Id = Guid.NewGuid();
                _unitOfWork.Genres.Create(_mapper.Map<Genre>(genreDto));
                _unitOfWork.Save();

                _log.Info($"{nameof(GenreService)} - add new genre { genreDto.Id}");
            }
            else
            {
                throw new NotUniqueParameter($"{nameof(GenreService)} - attempt to add new genre with not unique name");
            }
        }
        private Genre TakeGenreById(Guid id)
        {
            var genre = _unitOfWork.Genres.GetById(id);

            if (genre == null)
                throw new EntityNotFound($"{nameof(GenreService)} - genre with such id {id} did not exist");

            return genre;
        }

        public void Update(GenreDTO genreDto)
        {
            TakeGenreById(genreDto.Id);

            var genre = _mapper.Map<Genre>(genreDto);

            _unitOfWork.Genres.Update(genre);
            _unitOfWork.Save();

            _log.Info($"{nameof(GenreService)} - update genre {genreDto.Id}");
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
            var genre = _unitOfWork.Genres.Get(x => x.Name == name).FirstOrDefault();
           
            if (genre == null)
            {
                throw new EntityNotFound($"{nameof(GenreService)} - genre with such name {name} did not exist");
            }

            var genresDto = _mapper.Map<GenreDTO>(genre);

            if (genre.ParentGenreId != null)
                genresDto.ParentGenreName = _unitOfWork.Genres.GetById(genre.ParentGenreId.Value).Name;

            return genresDto;
        }
    }
}
