﻿using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Exeption;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.BLL.Service
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILog _log;
        private readonly IMapper _mapper;

        private static readonly string ExcInReturningGameBy =
            $"{nameof(GameService)} - exception in returning games by";

        public GameService(IUnitOfWork uow, IMapper mapper, ILog log)
        {
            _unitOfWork = uow;
            _mapper = mapper;
            _log = log;
        }

        public bool AddNew(GameDTO gameDto)
        {
            if (IsUniqueKey(gameDto))
            {
                gameDto.Id = Guid.NewGuid();
                var newGame = _mapper.Map<Game>(gameDto);
                newGame.Genres = _unitOfWork.Genres.Get(genre => gameDto.SelectedGenresName.Contains(genre.Name)).ToList();
                newGame.PlatformTypes = _unitOfWork.PlatformTypes
                    .Get(platformType => gameDto.SelectedPlatformTypesName.Contains(platformType.Name)).ToList();

                _unitOfWork.Games.Create(newGame);
                _unitOfWork.Save();

                _log.Info($"{nameof(GameService)} - add new game {gameDto.Id}");

                return true;
            }
            else
            {
                _log.Info($"{nameof(GameService)} - attempt to add new game with not unique key, {gameDto.Key}");

                return false;
            }
        }

        public void Delete(Guid id)
        {
            if (TakeGameById(id) != null)
            {
                _unitOfWork.Games.Delete(id);
                _unitOfWork.Save();

                _log.Info($"{nameof(GameService)} - delete game {id}");
            }
        }

        public bool Update(GameDTO gameDto)
        {
            if (IsUniqueKey(gameDto))
            {
                var game = TakeGameById(gameDto.Id);

                if (game != null)
                {
                    game.Genres.Clear();
                    game.PlatformTypes.Clear();

                    game.Genres = _unitOfWork.Genres.Get(genre => gameDto.SelectedGenresName.Contains(genre.Name)).ToList();
                    game.PlatformTypes = _unitOfWork.PlatformTypes
                        .Get(platformType => gameDto.SelectedPlatformTypesName.Contains(platformType.Name)).ToList();

                    _unitOfWork.Games.Update(game);
                    game = _mapper.Map<Game>(gameDto);
                    _unitOfWork.Games.Update(game);
                    _unitOfWork.Save();

                    _log.Info($"{nameof(GameService)} - update game {gameDto.Id}");

                    return true;
                }
            }

            _log.Info($"{nameof(GenreService)} - attempt to update game with not unique key, {gameDto.Key}");

            return false;
        }

        public IEnumerable<GameDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<GameDTO>>(_unitOfWork.Games.GetAll());
        }

        public GameDTO GetByKey(string gamekey)
        {
            var game = _unitOfWork.Games.Get(g => g.Key == gamekey).FirstOrDefault();

            if (game == null)
            {
                throw new EntityNotFound($"{nameof(GameService)} - game with such gamekey {gamekey} did not exist");
            }
            return _mapper.Map<GameDTO>(game);
        }

        public GameDTO GetById(Guid id)
        {
            var game = TakeGameById(id);

            return _mapper.Map<GameDTO>(game);
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
                throw new EntityNotFound($"{ExcInReturningGameBy} genre id, such genre id {genreId} did not exist");
            }

            return _mapper.Map<IEnumerable<GameDTO>>(gamesListByGenre);
        }

        public IEnumerable<GameDTO> GetGamesByPlatformType(Guid platformTypeId)
        {
            IEnumerable<Game> gamesListByPlatformType;
            var platformType = _unitOfWork.PlatformTypes.GetById(platformTypeId);

            if (platformType != null)
            {
                gamesListByPlatformType =
                    _unitOfWork.Games.Get(game => game.PlatformTypes.Any(x => x.Id == platformTypeId));
            }
            else
            {
                throw new EntityNotFound(
                    $"{ExcInReturningGameBy} platform type id, such platform type id {platformTypeId} did not exist");
            }

            return _mapper.Map<IEnumerable<GameDTO>>(gamesListByPlatformType);
        }

        public int GetCountGame()
        {
            return _unitOfWork.Games.Count();
        }

        private bool IsUniqueKey(GameDTO gameDTO)
        {
            var game = _unitOfWork.Games.Get(x => x.Key == gameDTO.Key).FirstOrDefault();

            if (game == null || gameDTO.Id == game.Id)
                return true;

            return false;
        }

        private Game TakeGameById(Guid id)
        {
            var game = _unitOfWork.Games.GetById(id);

            if (game == null)
                throw new EntityNotFound($"{nameof(GameService)} - attempt to take not existed game, id {id}");

            return game;
        }
    }
}

