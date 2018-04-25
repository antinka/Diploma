using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Exeption;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Interfaces;
using System;
using System.Collections.Generic;

namespace GameStore.BLL.Service
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenreService(IUnitOfWork uow, IMapper mapper)
        {
            _unitOfWork = uow;
            _mapper = mapper;
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

            return _mapper.Map<IEnumerable<GenreDTO>>(genres);
        }
    }
}
