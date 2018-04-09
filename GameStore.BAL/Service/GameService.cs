using AutoMapper;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.BLL.DTO;
using GameStore.BLL.Exeption;
using GameStore.BLL.Interfaces;

namespace GameStore.BLL.Service
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILog _log;
        private readonly IMapper _mapper;

        public GameService(IUnitOfWork uow, IMapper mapper, ILog log)
        {
            _unitOfWork = uow;
            _mapper = mapper;
            _log = log;
        }

        public void AddNew(GameDTO gameDto)
        {
            var game = _unitOfWork.Games.Get(x => x.Key == gameDto.Key);

            if (game == null)
            {
                gameDto.Id = Guid.NewGuid();
                _unitOfWork.Games.Create(_mapper.Map<Game>(gameDto));
                _unitOfWork.Save();

                _log.Info($"{nameof(GameService)} - add new game{ gameDto.Id}");
            }
            else
            {
                throw new EntityNotFound($"{nameof(GameService)} - attempt to add new game with not unique key");
            }
        }

        public void Delete(Guid id)
        {
            var game = _unitOfWork.Games.GetById(id);

            if (game == null)
                throw new EntityNotFound($"{nameof(GameService)} - attempt to delete not existed game, id {id}");

            _unitOfWork.Games.Delete(id);
            _unitOfWork.Save();

            _log.Info($"{nameof(GameService)} - delete game{id}");
        }

        public void Update(GameDTO gameDto)
        {
            _unitOfWork.Games.Update(_mapper.Map<Game>(gameDto));
            _unitOfWork.Save();

            _log.Info($"{nameof(GameService)} - update game{gameDto.Id}");
        }

        public IEnumerable<GameDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<GameDTO>>(_unitOfWork.Games.GetAll()).ToList();
        }

        public GameDTO Get(Guid id)
        {
            var game = _unitOfWork.Games.GetById(id);

            if (game == null)
            {
                throw new EntityNotFound($"{nameof(GameService)} - game with such id {id} did not exist");
            }
            else
            {
                return _mapper.Map<GameDTO>(game);
            }
        }

        public IEnumerable<GameDTO> GetGamesByGenre(Guid genreId)
        {
            IEnumerable<Game> gamesListByGenre;
            var genre = _unitOfWork.Genres.GetById(genreId);

            if (genre != null)
            {
                gamesListByGenre = _unitOfWork.Games.Get(game => game.Genres.Any(x => x.Id == genreId));
            }
            else
            {
                throw new EntityNotFound($"{nameof(GameService)} - exception in returning all games by GenreId, such genre id {genreId} did not exist");
            }

            return _mapper.Map<IEnumerable<GameDTO>>(gamesListByGenre);
        }

        public IEnumerable<GameDTO> GetGamesByPlatformType(Guid platformTypeId)
        {
            IEnumerable<Game> gamesListByPlatformType;
            var platformType = _unitOfWork.PlatformTypes.GetById(platformTypeId);

            if (platformType != null)
            {
                gamesListByPlatformType = _unitOfWork.Games.Get(game => game.PlatformTypes.Any(x => x.Id == platformTypeId));
            }
            else
            {
                throw new EntityNotFound($"{nameof(GameService)}- exception in returning all games by platformTypeId, such platform type id {platformTypeId} did not exist");
            }

            return _mapper.Map<IEnumerable<GameDTO>>(gamesListByPlatformType);
        }
    }
}
