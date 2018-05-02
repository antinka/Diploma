using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Enums;
using GameStore.BLL.Exeption;
using GameStore.BLL.Filtration.Implementation;
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
            $"{nameof(GameService)} - exception in returning all games by";

        public GameService(IUnitOfWork uow, IMapper mapper, ILog log)
        {
            _unitOfWork = uow;
            _mapper = mapper;
            _log = log;
        }

        public void AddNew(GameDTO gameDto)
        {
            var game = _unitOfWork.Games.Get(x => x.Key == gameDto.Key).FirstOrDefault();

            if (game == null)
            {
                gameDto.Id = Guid.NewGuid();
                var newGame = _mapper.Map<Game>(gameDto);
                newGame.Genres = _unitOfWork.Genres.Get(genre => gameDto.GenresId.Contains(genre.Id)).ToList();
                newGame.PlatformTypes = _unitOfWork.PlatformTypes
                    .Get(platformType => gameDto.PlatformTypesId.Contains(platformType.Id)).ToList();

                _unitOfWork.Games.Create(newGame);
                _unitOfWork.Save();

                _log.Info($"{nameof(GameService)} - add new game{gameDto.Id}");
            }
            else
            {
                throw new NotUniqueParameter($"{nameof(GameService)} - attempt to add new game with not unique key");
            }
        }

        private Game TakeGameById(Guid id)
        {
            var game = _unitOfWork.Games.GetById(id);

            if (game == null)
                throw new EntityNotFound($"{nameof(GameService)} - game with such id {id} did not exist");

            return game;
        }

        public void Delete(Guid id)
        {
            TakeGameById(id);

            _unitOfWork.Games.Delete(id);
            _unitOfWork.Save();

            _log.Info($"{nameof(GameService)} - delete game{id}");
        }

        public void Update(GameDTO gameDto)
        {
            var game = _mapper.Map<Game>(gameDto);

            _unitOfWork.Games.Update(game);

            game = TakeGameById(gameDto.Id);

            game.Genres.Clear();
            game.PlatformTypes.Clear();

            game.Genres = _unitOfWork.Genres.Get(genre => gameDto.SelectedGenresName.Contains(genre.Name)).ToList();
            game.PlatformTypes = _unitOfWork.PlatformTypes
                .Get(platformType => gameDto.SelectedPlatformTypesName.Contains(platformType.Name)).ToList();

            _unitOfWork.Games.Update(game);

            _unitOfWork.Save();

            _log.Info($"{nameof(GameService)} - update game{gameDto.Id}");
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
            else
            {
                return _mapper.Map<GameDTO>(game);
            }
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
                throw new EntityNotFound($"{ExcInReturningGameBy} GenreId, such genre id {genreId} did not exist");
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
                    $"{ExcInReturningGameBy} platformTypeId, such platform type id {platformTypeId} did not exist");
            }

            return _mapper.Map<IEnumerable<GameDTO>>(gamesListByPlatformType);
        }

        public int GetCountGame()
        {
            return _unitOfWork.Games.Count();
        }

        public IEnumerable<GameDTO> GetGamesByFilter(FilterDTO filter, int page = 1, PageSize pageSize = PageSize.Twenty)
        {
            GamePipeline gamePipeline = new GamePipeline();
            RegisterFilter(gamePipeline, filter, page, pageSize);
            var filterGames = gamePipeline.Process(_unitOfWork.Games.GetAll());

            return _mapper.Map<IEnumerable<GameDTO>>(filterGames);
        }

        private void RegisterFilter(GamePipeline gamePipeline, FilterDTO filter, int page, PageSize pageSize)
        {
            if (filter.SelectedGenresName != null && filter.SelectedGenresName.Any())
            {
                gamePipeline.Register(new FilterByGenre(filter.SelectedGenresName));
            }

            if (filter.MaxPrice != null)
            {
                gamePipeline.Register(new FilterByMaxPrice(filter.MaxPrice.Value));
            }

            if (filter.MinPrice != null)
            {
                gamePipeline.Register(new FilterByMinPrice(filter.MinPrice.Value));
            }

            if (filter.SelectedPlatformTypesName != null && filter.SelectedPlatformTypesName.Any())
            {
                gamePipeline.Register(new FilterByPlatform(filter.SelectedPlatformTypesName));
            }

            if (filter.SelectedPublishersName != null && filter.SelectedPublishersName.Any())
            {
                gamePipeline.Register(new FilterByPublisher(filter.SelectedPublishersName));
            }

            if (filter.SearchGameName != null && filter.SearchGameName.Length >= 3)
            {
                gamePipeline.Register(new FilterByName(filter.SearchGameName));
            }

            gamePipeline.Register(new FilterByDate(filter.SortDate))
                .Register(new SortFilter(filter.SortType))
                .Register(new FilterByPage(page, pageSize));

        }
    }
}

