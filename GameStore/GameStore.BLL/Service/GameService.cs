﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameStore.BLL.CustomExeption;
using GameStore.BLL.DTO;
using GameStore.BLL.Enums;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using log4net;
using GameStore.BLL.Filters.GameFilters.Implementation;
using CollaborativeFilteringExamples;

namespace GameStore.BLL.Service
{
    public class GameService : IGameService
    {
        private static readonly string ExcInReturningGameBy =
            $"{nameof(GameService)} - exception in returning games by";

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILog _log;
        private readonly IMapper _mapper;

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
            newGame.Genres = _unitOfWork.Genres.Get(genre => gameDto.SelectedGenresName.Contains(genre.NameEn)
            || gameDto.SelectedGenresName.Contains(genre.NameRu)).ToList();
            newGame.PlatformTypes = _unitOfWork.PlatformTypes
                .Get(platformType => gameDto.SelectedPlatformTypesName.Contains(platformType.NameEn)
                || gameDto.SelectedPlatformTypesName.Contains(platformType.NameRu)).ToList();
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

                game.Genres = _unitOfWork.Genres.Get(genre => gameDto.SelectedGenresName.Contains(genre.NameEn)
                || gameDto.SelectedGenresName.Contains(genre.NameRu)).ToList();
                game.PlatformTypes = _unitOfWork.PlatformTypes
                    .Get(platformType => gameDto.SelectedPlatformTypesName.Contains(platformType.NameEn)
                    || gameDto.SelectedPlatformTypesName.Contains(platformType.NameRu)).ToList();

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

            return _mapper.Map<IEnumerable<GameDTO>>(games);
        }

        public ExtendGameDTO GetByKey(string gamekey)
        {
            var game = _unitOfWork.Games.Get(g => g.Key == gamekey).FirstOrDefault();

            if (game == null)
            {
                throw new EntityNotFound($"{nameof(GameService)} - game with such gamekey {gamekey} did not exist");
            }

            return _mapper.Map<ExtendGameDTO>(game);
        }

        public GameDTO GetById(Guid id)
        {
            var game = GetGameById(id);
            IncreaseGameView(game);

            return _mapper.Map<GameDTO>(game);
        }

        public IEnumerable<GameDTO> GetDeleteGames()
        {
            var deleteGames = _unitOfWork.Games.Get(g => g.IsDelete == true);

            return _mapper.Map<IEnumerable<GameDTO>>(deleteGames);
        }

        public IEnumerable<GameDTO> GetGamesByPublisherId(Guid publisherId)
        {
            var publisherName = _unitOfWork.Publishers.Get(x => x.Id == _unitOfWork.Users.GetById(publisherId).PublisherId)
                .FirstOrDefault()
                ?.Name;

            if (publisherName == null)
            {
                return null;
            }

            var games = _unitOfWork.Games.Get(game =>
                game.Publisher != null && publisherName.Contains(game.Publisher.Name) && game.IsDelete == false);

            return _mapper.Map<IEnumerable<GameDTO>>(games);
        }

        public ExtendGameDTO GetGameByPublisherIdAndGameKey(Guid publisherId, string gameKey)
        {
            var publisherName = _unitOfWork.Publishers.Get(x => x.Id == _unitOfWork.Users.GetById(publisherId).PublisherId)
                .FirstOrDefault()
                ?.Name;

            if (publisherName == null)
            {
                return null;
            }

            var game = _unitOfWork.Games.Get(g =>
                g.Publisher != null && publisherName.Contains(g.Publisher.Name) && g.Key == gameKey).FirstOrDefault();

            return _mapper.Map<ExtendGameDTO>(game);
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

        public void Renew(string gameKey)
        {
            var game = _unitOfWork.Games.Get(g => g.Key == gameKey).FirstOrDefault();

            if (game == null)
            {
                throw new EntityNotFound($"{nameof(GameService)} - game with such gamekey {gameKey} did not exist");
            }

            game.IsDelete = false;
            _unitOfWork.Games.Update(game);
            _unitOfWork.Save();
        }

        public IEnumerable<GameDTO> GetGamesByFilter(FilterDTO filter, int page, PageSize pageSize, out int totalItemsByFilter)
        {
            var games = _unitOfWork.Games.GetAll();

            var filterGamePipeline = new GamePipeline();
            RegisterFilter(filterGamePipeline, filter, page, pageSize);

            var filterGames = filterGamePipeline.Process(games).ToList();

            var totalItemsByFilteripeline = new GamePipeline();
            RegisterFilter(totalItemsByFilteripeline, filter, 1, PageSize.All);
            totalItemsByFilter = totalItemsByFilteripeline.Process(games).Count();

            return _mapper.Map<IEnumerable<GameDTO>>(filterGames);
        }

        public bool IsUniqueKey(ExtendGameDTO gameExtendGameDto)
        {
            var game = _unitOfWork.Games.Get(x => x.Key == gameExtendGameDto.Key).FirstOrDefault();

            if (game == null || gameExtendGameDto.Id == game.Id)
            {
                return true;
            }

            return false;
        }

        public void UpdateImage(string gameKey, string pictureName, string imageMimeType)
        {
            var game = _unitOfWork.Games.Get(g => g.Key == gameKey).FirstOrDefault();
            game.ImageName = pictureName;
            game.ImageMimeType = imageMimeType;

            _unitOfWork.Games.Update(game);
            _unitOfWork.Save();
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
            {
                throw new EntityNotFound($"{nameof(GameService)} - attempt to take not existed game, id {id}");
            }

            return game;
        }

        public IEnumerable<GameDTO> GetGamesByCollaborative(Guid gamekey, UserDTO userDTO = null)
        {
            //var user = _unitOfWork.Users.Get(x => x.IsWoman == userDTO.IsWoman && x.Adulthood == userDTO.Adulthood);

            var user = _unitOfWork.Users.GetAll();
            UserProfile currentUserProfile = new UserProfile(Guid.NewGuid(), new Guid[] { gamekey });
            var profiles = new List<UserProfile>();

            for (int i = 0; i < user.Count(); i++)
            {
                var orders = _unitOfWork.Orders.Get(u => u.UserId == user.ToArray()[i].Id);
                var orderDet = _unitOfWork.OrderDetails.Get(od => orders.Any(o => od.OrderId == o.Id));
                var games = _unitOfWork.Games.Get(g => orderDet.Any(o => g.Id == o.GameId));

                var gamesId = new Guid[games.Count()];

                for (int j = 0; j < games.Count(); j++)
                {
                    gamesId[j] = games.ToArray()[j].Id;
                }


                if (user.ToArray()[i].Id == userDTO.Id)
                {
                    var gamesIds = new Guid[games.Count() + 1];
                    for (int j = 0; j < gamesId.Count(); j++)
                    {
                        gamesIds[j] = gamesId[j];
                    }
                    gamesIds[games.Count()] = gamekey;
                    currentUserProfile = new UserProfile(user.ToArray()[i].Id, gamesId);
                }

                profiles.Add(new UserProfile(user.ToArray()[i].Id, gamesId));
            }


            var simiarity = new JaccardSimilarity();
            var engine = new CollaborativeFiltering();

            var results = engine.recommend(profiles, simiarity, currentUserProfile);

            var games3 = _unitOfWork.Games.Get(x => results.Any(r => x.Id == r.Key));


            if (results.ElementAt(0).Value == 0)
            {
                var user2 = user.Where(x => x.IsWoman == userDTO.IsWoman && x.Adulthood == userDTO.Adulthood);
                var profiles2 = new List<UserProfile>();

                for (int i = 0; i < user2.Count(); i++)
                {
                    var orders = _unitOfWork.Orders.Get(u => u.UserId == user.ToArray()[i].Id);
                    var orderDet = _unitOfWork.OrderDetails.Get(od => orders.Any(o => od.OrderId == o.Id));
                    var games = _unitOfWork.Games.Get(g => orderDet.Any(o => g.Id == o.GameId));

                    var gamesId = new Guid[games.Count()];

                    for (int j = 0; j < games.Count(); j++)
                    {
                        gamesId[j] = games.ToArray()[j].Id;
                    }


                    if (user2.ToArray()[i].Id == userDTO.Id)
                    {
                        currentUserProfile = new UserProfile(user.ToArray()[i].Id, gamesId);
                    }

                    profiles.Add(new UserProfile(user.ToArray()[i].Id, gamesId));
                }

                var simiarity2 = new JaccardSimilarity();
                var engine2 = new CollaborativeFiltering();

                var results2 = engine.recommend(profiles, simiarity, currentUserProfile);

                var games2 = _unitOfWork.Games.Get(x => results.Any(r => x.Id == r.Key));
                return _mapper.Map<IEnumerable<GameDTO>>(games3);

            }
            return _mapper.Map<IEnumerable<GameDTO>>(games3);
        }

        public IEnumerable<GameDTO> GetGamesByContent(Guid gamekey, UserDTO userDTO = null)
        {
            var user = _unitOfWork.Users.Get(x => x.Id == userDTO.Id).FirstOrDefault();

            var orders = _unitOfWork.Orders.Get(u => u.UserId == userDTO.Id);
            var orderDet = _unitOfWork.OrderDetails.Get(od => orders.Any(o => od.OrderId == o.Id));
            var games = _unitOfWork.Games.Get(g => orderDet.Any(o => g.Id == o.GameId)).ToList();
            if (games.Where(x => x.Id == gamekey).Count() == 0)
            {
                games.Add(_unitOfWork.Games.Get(g => g.Id == gamekey).FirstOrDefault());
            }

            var ggg = _unitOfWork.Games.Get(x => games.Any(p => x.Publisher == p.Publisher && x.PlatformTypes == p.PlatformTypes && x.Genres == p.Genres)).Except(games);
            if (ggg.Count() < 3)
            {
                ggg = _unitOfWork.Games.Get(x => games.Any(p => x.Publisher == p.Publisher || x.PlatformTypes == p.PlatformTypes || x.Genres == p.Genres)).Except(games);
            }


            return _mapper.Map<IEnumerable<GameDTO>>(ggg);
        }
    }
}