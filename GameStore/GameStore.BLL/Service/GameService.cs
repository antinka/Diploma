using AutoMapper;
using GameStore.BLL.CustomExeption;
using GameStore.BLL.DTO;
using GameStore.BLL.Enums;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.BLL.Filters.GameFilters.Implementation;

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

        public void AddNew(ExtendGameDTO gameDto)
        {
            gameDto.Id = Guid.NewGuid();
            var newGame = _mapper.Map<Game>(gameDto);
            newGame.Genres = _unitOfWork.Genres.Get(genre => gameDto.SelectedGenresName.Contains(genre.NameEn) || gameDto.SelectedGenresName.Contains(genre.NameRu)).ToList();
            newGame.PlatformTypes = _unitOfWork.PlatformTypes
                .Get(platformType => gameDto.SelectedPlatformTypesName.Contains(platformType.NameEn) || gameDto.SelectedPlatformTypesName.Contains(platformType.NameRu)).ToList();
            newGame.PublishDate = DateTime.UtcNow;

            _unitOfWork.Games.Create(newGame);
            _unitOfWork.Save();


            _log.Info($"{nameof(GameService)} - add new game {gameDto.Id}");
        }

        public void Delete(Guid id)
        {
            if (GetGameById(id) != null)
            {
                _unitOfWork.Games.Delete(id);
                _unitOfWork.Save();

                _log.Info($"{nameof(GameService)} - delete game {id}");
            }
        }

        public void Update(ExtendGameDTO gameDto)
        {
            var game = GetGameById(gameDto.Id);

            if (game != null)
            {

                if (game.Price != gameDto.Price)
                {
                    var updateOrderDetails = _unitOfWork.OrderDetails.Get(g => g.Game.Key == gameDto.Key).ToList();
                    foreach (var orderDetails in updateOrderDetails)
                    {
                        orderDetails.Price = gameDto.Price * orderDetails.Quantity;
                        _unitOfWork.OrderDetails.Update(orderDetails);
                    }
                }

                game.Genres.Clear();
                game.PlatformTypes.Clear();

                game.Genres = _unitOfWork.Genres.Get(genre => gameDto.SelectedGenresName.Contains(genre.NameEn) || gameDto.SelectedGenresName.Contains(genre.NameRu)).ToList();
                game.PlatformTypes = _unitOfWork.PlatformTypes
                    .Get(platformType => gameDto.SelectedPlatformTypesName.Contains(platformType.NameEn) || gameDto.SelectedPlatformTypesName.Contains(platformType.NameRu)).ToList();

                _unitOfWork.Games.Update(game);
                game = _mapper.Map<Game>(gameDto);
                _unitOfWork.Games.Update(game);
                _unitOfWork.Save();

                _log.Info($"{nameof(GameService)} - update game {gameDto.Id}");
            }
        }

        public IEnumerable<GameDTO> GetAll()
        {
            var games = _unitOfWork.Games.GetAll().ToList();

            for (int i = 0; i < games.Count(); i++)
            {
                if (!games[i].Genres.Any())
                    games[i] = AddDefaultGenre(games[i]);
            }

            return _mapper.Map<IEnumerable<GameDTO>>(games);
        }

        public ExtendGameDTO GetByKey(string gamekey)
        {
            var game = _unitOfWork.Games.Get(g => g.Key == gamekey).FirstOrDefault();

            if (game == null)
            {
                throw new EntityNotFound($"{nameof(GameService)} - game with such gamekey {gamekey} did not exist");
            }

            if (!game.Genres.Any())
                game = AddDefaultGenre(game);

            return _mapper.Map<ExtendGameDTO>(game);
        }

        public GameDTO GetById(Guid id)
        {
            var game = GetGameById(id);
            IncreaseGameView(game);

            if (!game.Genres.Any())
                game = AddDefaultGenre(game);

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

        public IEnumerable<GameDTO> GetGamesByFilter(FilterDTO filter, int page, PageSize pageSize, out int totalItemsByFilter)
        {
            var games = _unitOfWork.Games.GetAll();

            var filterGamePipeline = new GamePipeline();
            RegisterFilter(filterGamePipeline, filter, page, pageSize);

            var filterGames = filterGamePipeline.Process(games).ToList();
            for (int i = 0; i < filterGames.Count(); i++)
            {
                if (!filterGames[i].Genres.Any())
                    filterGames[i] = AddDefaultGenre(filterGames[i]);
            }

            var totalItemsByFilteripeline = new GamePipeline();
            RegisterFilter(totalItemsByFilteripeline, filter, 1, PageSize.All);
            totalItemsByFilter = totalItemsByFilteripeline.Process(games).Count();

            return _mapper.Map<IEnumerable<GameDTO>>(filterGames);
        }

        public bool IsUniqueKey(ExtendGameDTO gameExtendGameDto)
        {
            var game = _unitOfWork.Games.Get(x => x.Key == gameExtendGameDto.Key).FirstOrDefault();

            if (game == null || gameExtendGameDto.Id == game.Id)
                return true;

            return false;
        }

        private void IncreaseGameView(Game game)
        {
            game.Views += 1;

            _unitOfWork.Games.Update(game);
            _unitOfWork.Save();
        }

        private void RegisterFilter(GamePipeline gamePipeline, FilterDTO filter, int page, PageSize pageSize)
        {
            if (filter.SelectedGenresName != null && filter.SelectedGenresName.Any())
            {
                gamePipeline.Register(new GameFilterByGenre(filter.SelectedGenresName));
            }

            if (filter.MaxPrice != null)
            {
                gamePipeline.Register(new GameFilterByPrice(filter.MaxPrice.Value, null));
            }

            if (filter.MinPrice != null)
            {
                gamePipeline.Register(new GameFilterByPrice(null, filter.MinPrice.Value));
            }

            if (filter.MaxPrice != null && filter.MinPrice != null)
            {
                gamePipeline.Register(new GameFilterByPrice(filter.MaxPrice.Value, filter.MinPrice.Value));
            }

            if (filter.SelectedPlatformTypesName != null && filter.SelectedPlatformTypesName.Any())
            {
                gamePipeline.Register(new GameFilterByPlatform(filter.SelectedPlatformTypesName));
            }

            if (filter.SelectedPublishersName != null && filter.SelectedPublishersName.Any())
            {
                gamePipeline.Register(new GameFilterByPublisher(filter.SelectedPublishersName));
            }

            if (filter.SearchGameName != null && filter.SearchGameName.Length >= 3)
            {
                gamePipeline.Register(new GameFilterByName(filter.SearchGameName));
            }

            if (filter.FilterDate != FilterDate.all)
            {
                gamePipeline.Register(new GameFilterByDate(filter.FilterDate));
            }

            gamePipeline.Register(new GameSortFilter(filter.SortType))
                .Register(new GameFilterByPage(page, pageSize));
        }

        private Game GetGameById(Guid id)
        {
            var game = _unitOfWork.Games.GetById(id);

            if (game == null)
                throw new EntityNotFound($"{nameof(GameService)} - attempt to take not existed game, id {id}");

            return game;
        }

        private Game AddDefaultGenre(Game game)
        {
            var genreDefault = new Genre()
            {
                NameRu = "Другие",
                NameEn = "Other"
            };

            game.Genres.Add(genreDefault);

            return game;
        }

    }
}

