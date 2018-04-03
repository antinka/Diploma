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
        public void AddNewGame(GameDTO gameDto)
        {
            var game = _unitOfWork.Games.Get(x => x.Key == gameDto.Key).FirstOrDefault();

            if (game == null)
            {
                gameDto.Id = Guid.NewGuid();
                _unitOfWork.Games.Create(_mapper.Map<Game>(gameDto));
                _unitOfWork.Save();

                _log.Info($"{nameof(GameService)} - add new game{ gameDto.Id}");
            }
            else
            {
                throw new EntityNotFound("GameService - attempt to add new game with not unique key");
            }
        }

        public void DeleteGame(Guid id)
        {
            var game = _unitOfWork.Games.GetById(id);

            if (game == null)
                throw new EntityNotFound("GameService - attempt to delete not existed game");

            _unitOfWork.Games.Delete(id);
            _unitOfWork.Save();

            _log.Info($"{nameof(GameService)} - delete game{id}");
        }

        public void UpdateGame(GameDTO gameDto)
        {
            _unitOfWork.Games.Update(_mapper.Map<Game>(gameDto));
            _unitOfWork.Save();

            _log.Info($"{nameof(GameService)} - update game{gameDto.Id}");
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
            }
            else
            {
                throw new EntityNotFound("CommentService - exception in returning all games by GenreId, such genre id did not exist");
            }

            return _mapper.Map<List<GameDTO>>(gamesListByGenre);
        }

        public IEnumerable<GameDTO> GetGamesByPlatformType(Guid platformTypeId)
        {
            IEnumerable<Game> gamesList;

            if (_unitOfWork.PlatformTypes.GetById(platformTypeId)!= null)
            {
                gamesList = _unitOfWork.Games.GetAll().ToList().Where(game => game.PlatformTypes.Any(x=>x.Id == platformTypeId)).ToList();
            }
            else
            {
                throw new EntityNotFound("CommentService - exception in returning all games by platformTypeId, such platform type id did not exist");
            }

            return _mapper.Map<List<GameDTO>>(gamesList);
        }

    }
}
