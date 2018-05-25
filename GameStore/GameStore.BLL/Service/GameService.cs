using AutoMapper;
using GameStore.BLL.CustomExeption;
using GameStore.BLL.DTO;
using GameStore.BLL.Enums;
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
            $"{nameof(GameService)} - exception in returning games by";

        public GameService(IUnitOfWork uow, IMapper mapper, ILog log)
        {
            _unitOfWork = uow;
            _mapper = mapper;
            _log = log;
        }

        public void AddNew(GameDTO gameDto)
        {
            gameDto.Id = Guid.NewGuid();
            var newGame = _mapper.Map<Game>(gameDto);
            newGame.Genres = _unitOfWork.Genres.Get(genre => gameDto.SelectedGenresName.Contains(genre.Name)).ToList();
            newGame.PlatformTypes = _unitOfWork.PlatformTypes
                .Get(platformType => gameDto.SelectedPlatformTypesName.Contains(platformType.Name)).ToList();
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

        public void Update(GameDTO gameDto)
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

                game.Genres = _unitOfWork.Genres.Get(genre => gameDto.SelectedGenresName.Contains(genre.Name)).ToList();
                game.PlatformTypes = _unitOfWork.PlatformTypes
                    .Get(platformType => gameDto.SelectedPlatformTypesName.Contains(platformType.Name)).ToList();

                _unitOfWork.Games.Update(game);
                game = _mapper.Map<Game>(gameDto);
                _unitOfWork.Games.Update(game);
                _unitOfWork.Save();

                _log.Info($"{nameof(GameService)} - update game {gameDto.Id}");
            }
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
            var game = GetGameById(id);

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

        public IEnumerable<GameDTO> GetGamesByFilter(FilterDTO filter, int page = 1, PageSize pageSize = PageSize.Twenty)
        {
            var gamePipeline = new GamePipeline();
            RegisterFilter(gamePipeline, filter, page, pageSize);
            var filterGames = gamePipeline.Process(_unitOfWork.Games.GetAll());

            return _mapper.Map<IEnumerable<GameDTO>>(filterGames);
        }

        public void IncreaseGameView(Guid gameId)
        {
            var game = GetGameById(gameId);
            game.Views += 1;

            _unitOfWork.Games.Update(game);
            _unitOfWork.Save();
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

        public bool IsUniqueKey(GameDTO gameDTO)
        {
            var game = _unitOfWork.Games.Get(x => x.Key == gameDTO.Key).FirstOrDefault();

            if (game == null || gameDTO.Id == game.Id)
                return true;

            return false;
        }

        private Game GetGameById(Guid id)
        {
            var game = _unitOfWork.Games.GetById(id);

            if (game == null)
                throw new EntityNotFound($"{nameof(GameService)} - attempt to take not existed game, id {id}");

            return game;
        }
    }
}

