using AutoMapper;
using GameStore.BAL.DTO;
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

        //  IMapper mapper = new MapperConfiguration(cfg => cfg.CreateMap<GameDTO, Game>()).CreateMapper();
       // private readonly IMapper Mapper;

        public GameService(IUnitOfWork uow)
        {
            db = uow;
        }
        public void AddNewGame(GameDTO gameDTO)
        {
            Game checkUniqueGameKey = db.Games.GetAll().Where(x =>x.Key == gameDTO.Key).FirstOrDefault();
            if (checkUniqueGameKey == null)
            {
                db.Games.Create(Mapper.Map<GameDTO, Game>(gameDTO));
                db.Save();
                log.Info("GameService - add new game");
            }
            else
            {
                log.Error("GameService - attempt to add new game with not unique key");
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
                log.Error("GameService - attempt to delete not existed game");
        }

        public void EditGame(GameDTO gameDTO)
        {
            db.Games.Update(Mapper.Map<GameDTO, Game>(gameDTO));
            db.Save();
            log.Info("GameService - update game");
        }

        public IEnumerable<GameDTO> GetAllGame()
        { 
            return Mapper.Map<IEnumerable<Game>, List<GameDTO>>(db.Games.GetAll());
        }

        public GameDTO GetGame(Guid id)
        { 
            return Mapper.Map<Game, GameDTO>(db.Games.Get(id));
        }

        public IEnumerable<GameDTO> GetGamesByGenre(Guid GenreId)
        {
            IEnumerable<Game> list = new List<Game>();
            try
            {
                list = (from gm in db.Games.GetAll()
                        from g in gm.Genres
                        where g.Id == GenreId
                        select gm).ToList();
                log.Info("GameService - return game to select genre");
            }
            catch(Exception ex)
            {
                log.Error("GameService - exception in returning game to select genre - " + ex.Message);
            }
            return Mapper.Map<IEnumerable<Game>, List<GameDTO>>(list);
        }

        public IEnumerable<GameDTO> GetGamesByPlatformType(Guid platformTypeId)
        {

            List<Game> list = new List<Game>();
            try
            {

                list = (from gm in db.Games.GetAll()
                       from p in gm.PlatformTypes
                       where p.Id == platformTypeId
                       select gm).ToList();

                log.Info("GameService - return game to platform type");
            }
            catch(Exception ex)
            {
                log.Error("GameService - exception in returning game to select platform type - " + ex.Message);
            }
            return Mapper.Map<IEnumerable<Game>, List<GameDTO>>(list);
        }

    }
}
