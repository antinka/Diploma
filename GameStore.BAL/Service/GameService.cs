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
using System.Text.RegularExpressions;

namespace GameStore.BAL.Service
{
    public class GameService : IGameService
    {
        private IUnitOfWork Db { get; set; }
        private readonly ILog _log = LogManager.GetLogger("LOGGER");
        private readonly IMapper _mapper = GameStore.Infastracture.MapperConfigBLL.GetMapper();

        public GameService(IUnitOfWork uow)
        {
            Db = uow;
        }
        public void AddNewGame(GameDTO gameDto)
        {
            Game checkUniqueGameKey = Db.Games.GetAll().Where(x =>x.Key == gameDto.Key).FirstOrDefault();
            if (checkUniqueGameKey == null)
            {
                Db.Games.Create(_mapper.Map<GameDTO, Game>(gameDto));
                Db.Save();
                _log.Info("GameService - add new game");
            }
            else
            {
                throw new GameStoreExeption("GameService - attempt to add new game with not unique key");
            }
        }

        public void DeleteGame(Guid id)
        {
         
                Game subject = Db.Games.Get(id);
                if (subject != null)
                {
                    Db.Games.Delete(id);
                    Db.Save();
                    _log.Info("GameService - delete game");
                }
                throw new GameStoreExeption("GameService - attempt to delete not existed game");
        }

        public void EditGame(GameDTO gameDto)
        {
            Db.Games.Update(_mapper.Map<GameDTO, Game>(gameDto));
            Db.Save();
            _log.Info("GameService - update game");
        }

        public IEnumerable<GameDTO> GetAllGame()
        { 
            return _mapper.Map<IEnumerable<Game>, List<GameDTO>>(Db.Games.GetAll());
        }

        public GameDTO GetGame(Guid id)
        { 
            return _mapper.Map<Game, GameDTO>(Db.Games.Get(id));
        }

        public IEnumerable<GameDTO> GetGamesByGenre(Guid genreId)
        {
            IEnumerable<Game> gamesListByGenre = new List<Game>();

            if (Db.Genres.Get(genreId) != null)
            {
                gamesListByGenre = Db.Games.GetAll().ToList().Where(game => game.Genres.Any(x => x.Id == genreId)).ToList();

                _log.Info("GameService - return game to select genre");
            }
            else
            {
                throw new GameStoreExeption("CommentService - exception in returning all games by GenreId, such genre id did not exist");
            }

            return _mapper.Map<IEnumerable<Game>, List<GameDTO>>(gamesListByGenre);
        }

        public IEnumerable<GameDTO> GetGamesByPlatformType(Guid platformTypeId)
        {
            IEnumerable<Game> gamesList = new List<Game>();

            if (Db.PlatformTypes.Get(platformTypeId)!= null)
            {
                gamesList = Db.Games.GetAll().ToList().Where(game => game.PlatformTypes.Any(x=>x.Id == platformTypeId)).ToList();

                _log.Info("GameService - return game to platform type");
            }
            else
            {
                throw new GameStoreExeption("CommentService - exception in returning all games by platformTypeId, such platform type id did not exist");
            }

            return _mapper.Map<IEnumerable<Game>, List<GameDTO>>(gamesList);
        }

    }
}
