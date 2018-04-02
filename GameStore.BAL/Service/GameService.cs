using AutoMapper;
using GameStore.BAL.DTO;
using GameStore.BAL.Exeption;
using GameStore.BAL.Infastracture;
using GameStore.BAL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.BAL.Service
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
        public void AddNewGame(GameDTO gameDto)
        {
            var game = _unitOfWork.Games.Get(x => x.Key == gameDto.Key).FirstOrDefault();

            if (game == null)
            {
                gameDto.Id = Guid.NewGuid();
                _unitOfWork.Games.Create(_mapper.Map<Game>(gameDto));
                _unitOfWork.Save();

                _log.Info("GameService - add new game "+ gameDto.Id);
            }
            else
            {
                throw new EntityNotFound("GameService - attempt to add new game with not unique key", _log);
            }
        }

        public void DeleteGame(Guid id)
        {
            var game = _unitOfWork.Games.GetById(id);

            if (game == null)
                throw new EntityNotFound("GameService - attempt to delete not existed game", _log);

            _unitOfWork.Games.Delete(id);
            _unitOfWork.Save();

            _log.Info("GameService - delete game " + id);
        }

        public void UpdateGame(GameDTO gameDto)
        {
            _unitOfWork.Games.Update(_mapper.Map<Game>(gameDto));
            _unitOfWork.Save();

            _log.Info("GameService - update game "+ gameDto.Id);
        }

        public IEnumerable<GameDTO> GetAllGame()
        {
             return _mapper.Map<List<GameDTO>>(_unitOfWork.Games.GetAll());
        }


        public GameDTO GetGame(Guid id)
        { 
            return _mapper.Map<GameDTO>(_unitOfWork.Games.GetById(id));
        }

        public IEnumerable<GameDTO> GetGamesByGenre(Guid genreId)
        {
            IEnumerable<Game> gamesListByGenre;

            if (_unitOfWork.Genres.GetById(genreId) != null)
            {
                gamesListByGenre = _unitOfWork.Games.GetAll().ToList().Where(game => game.Genres.Any(x => x.Id == genreId));

                _log.Info("GameService - return game to select genre "+ genreId);
            }
            else
            {
                throw new EntityNotFound("CommentService - exception in returning all games by GenreId, such genre id did not exist", _log);
            }

            return _mapper.Map<List<GameDTO>>(gamesListByGenre);
        }

        public IEnumerable<GameDTO> GetGamesByPlatformType(Guid platformTypeId)
        {
            IEnumerable<Game> gamesList;

            if (_unitOfWork.PlatformTypes.GetById(platformTypeId)!= null)
            {
                gamesList = _unitOfWork.Games.GetAll().ToList().Where(game => game.PlatformTypes.Any(x=>x.Id == platformTypeId)).ToList();

                _log.Info("GameService - return game to platform type "+ platformTypeId);
            }
            else
            {
                throw new EntityNotFound("CommentService - exception in returning all games by platformTypeId, such platform type id did not exist", _log);
            }

            return _mapper.Map<List<GameDTO>>(gamesList);
        }

    }
}
