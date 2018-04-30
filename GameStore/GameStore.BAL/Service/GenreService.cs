using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using GameStore.BLL.Exeption;

namespace GameStore.BLL.Service
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILog _log;
        private readonly IMapper _mapper;

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
            else
            {
                return _mapper.Map<GenreDTO>(genre);
            }
        }

        public IEnumerable<GenreDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<GenreDTO>>(_unitOfWork.Genres.GetAll());
        }
    }
}
