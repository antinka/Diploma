using AutoMapper;
using GameStore.BAL.DTO;
using GameStore.BAL.Service;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using log4net;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GameStore.Tests.Service
{
    public class GameServiceTest
    {
        private static readonly Mock<IUnitOfWork> GameRepo = new Mock<IUnitOfWork>();
        private static readonly Mock<IMapper> Mapper = new Mock<IMapper>();
        private static readonly Mock<ILog> Log = new Mock<ILog>();
        private static readonly GameService GameService = new GameService(GameRepo.Object, Mapper.Object, Log.Object);

        private readonly List<Game> _games = new List<Game>();
        private bool _isUpdate;
        private readonly Guid _id = Guid.NewGuid();

        public GameServiceTest()
        {
            GameRepo.Setup(x => x.Games.GetAll()).Returns(new List<Game>
           {
                new Game (),
                new Game (),
                new Game ()
           });
            GameRepo.Setup(x => x.Games.Create(It.IsAny<Game>())).Callback(() => _games.Add(It.IsAny<Game>()));
            GameRepo.Setup(x => x.Games.GetById(_id)).Returns(new Game { Id = _id, Name = "game1" });
            GameRepo.Setup(x => x.Games.Update(It.IsAny<Game>())).Callback(() => _isUpdate = true);
        }

        [Fact]
        public void GetAllGame_3Games_3Games()
        {
            var countGames = GameService.GetAllGame().Count();

            Assert.Equal(3, countGames);
        }

        [Fact]
        public void AddNewGame_2GamesWithUniqKey_2Games()
        {
            var game1 = new GameDTO
            {
                Key = "1"
            };
            var game2 = new GameDTO
            {
                Key = "2"
            };
            GameService.AddNewGame(game1);
            GameService.AddNewGame(game2);

            GameRepo.Setup(x => x.Games.GetAll()) .Returns(_games);
            var countGames = GameService.GetAllGame().Count();

            Assert.Equal(2, countGames);
        }

        [Fact]
        public void EditGame_GameDTO_updateDescription()
        {
            var testGame = new GameDTO();

            GameService.UpdateGame(testGame);

            Assert.True(_isUpdate);
        }
       
            
    }
}
