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
using System.Security.Cryptography.X509Certificates;

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

            if (game.Count() == 0)
            {
                gameDto.Id = Guid.NewGuid();
                Game newGame = _mapper.Map<Game>(gameDto);
                newGame.Genres = _unitOfWork.Genres.Find(genre => gameDto.GenresId.Contains(genre.Id)).ToList();
                newGame.PlatformTypes = _unitOfWork.PlatformTypes
                    .Find(platformType => gameDto.PlatformTypesId.Contains(platformType.Id)).ToList();

                _unitOfWork.Games.Create(newGame);
                _unitOfWork.Save();

                _log.Info($"{nameof(GameService)} - add new game{ gameDto.Id}");
            }
            else
            {
                throw new EntityNotFound($"{nameof(GameService)} - attempt to add new game with not unique key");
            }
        }

        public void Delete(string gamekey)
        {
            var gameExist = _unitOfWork.Games.Get(game => game.Key == gamekey);

            if (gameExist == null)
                throw new EntityNotFound($"{nameof(GameService)} - attempt to delete not existed game, gamekey {gamekey}");

            _unitOfWork.Games.Delete(gameExist.First().Id);
            _unitOfWork.Save();

            _log.Info($"{nameof(GameService)} - delete game gamekey {gamekey}");
        }

        public void Update(GameDTO gameDto)
        {
            _unitOfWork.Games.Update(_mapper.Map<Game>(gameDto));
            _unitOfWork.Save();

            _log.Info($"{nameof(GameService)} - update game{gameDto.Id}");
        }

        public IEnumerable<GameDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<GameDTO>>(_unitOfWork.Games.GetAll());
        }

        public GameDTO Get(Guid id)
        {
            var gameExist = _unitOfWork.Games.Get(game => game.Key == gamekey).First();

            if (gameExist == null)
            {
                throw new EntityNotFound($"{nameof(GameService)} - game with such gamekey {gamekey} did not exist");
            }
            else
            {
                return _mapper.Map<GameDTO>(gameExist);
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
