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
        IUnitOfWork db { get; set; }
        ILog log = LogManager.GetLogger("LOGGER");
        IMapper mapper = GameStore.Infastracture.MapperConfigBLL.GetMapper();

        public GameService(IUnitOfWork uow)
        {
            db = uow;
        }
        public void AddNewGame(GameDTO gameDTO)
        {
            Game checkUniqueGameKey = db.Games.GetAll().Where(x =>x.Key == gameDTO.Key).FirstOrDefault();
            if (checkUniqueGameKey == null)
            {
                db.Games.Create(mapper.Map<GameDTO, Game>(gameDTO));
                db.Save();
                log.Info("GameService - add new game");
            }
            else
            {
                throw new GameStoreExeption("GameService - attempt to add new game with not unique key");
            }
        }

        public void DeleteGame(Guid id)
        {
         
                Game subject = db.Games.Get(id);
                if (subject != null)
                {
                    db.Games.Delete(id);
                    db.Save();
                    log.Info("GameService - delete game");
                }
                throw new GameStoreExeption("GameService - attempt to delete not existed game");
        }

        public void EditGame(GameDTO gameDTO)
        {
            db.Games.Update(Mapper.Map<GameDTO, Game>(gameDTO));
            db.Save();
            log.Info("GameService - update game");
        }

        public IEnumerable<GameDTO> GetAllGame()
        { 
            return mapper.Map<IEnumerable<Game>, List<GameDTO>>(db.Games.GetAll());
        }

        public GameDTO GetGame(Guid id)
        { 
            return mapper.Map<Game, GameDTO>(db.Games.Get(id));
        }

        public IEnumerable<GameDTO> GetGamesByGenre(Guid GenreId)
        {
            IEnumerable<Game> list = new List<Game>();
            if (db.Genres.Get(GenreId) != null)
            {
                list = (from gm in db.Games.GetAll()
                        from g in gm.Genres
                        where g.Id == GenreId
                        select gm).ToList();
                log.Info("GameService - return game to select genre");
            }
            else
            {
                throw new GameStoreExeption("CommentService - exception in returning all games by GenreId, such genre id did not exist");
            }
            return mapper.Map<IEnumerable<Game>, List<GameDTO>>(list);
        }

        public IEnumerable<GameDTO> GetGamesByPlatformType(Guid platformTypeId)
        {

            List<Game> list = new List<Game>();
            if (db.PlatformTypes.Get(platformTypeId) != null)
            {
                list = (from gm in db.Games.GetAll()
                       from p in gm.PlatformTypes
                       where p.Id == platformTypeId
                       select gm).ToList();

                log.Info("GameService - return game to platform type");
            }
            else
            {
                throw new GameStoreExeption("CommentService - exception in returning all games by platformTypeId, such platform type id did not exist");
            }
            return mapper.Map<IEnumerable<Game>, List<GameDTO>>(list);
        }

    }
}
