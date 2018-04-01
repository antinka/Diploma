
using GameStore.BAL.DTO;
using GameStore.BAL.Service;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GameStore.Tests.Service
{
    public class GameServiceTest
    {
        private static readonly Mock<IUnitOfWorkGeneric> GameRepo = new Mock<IUnitOfWorkGeneric>();
        private static readonly GameService GameService = new GameService(GameRepo.Object);

        private readonly List<Game> _games = new List<Game>();
        bool _boolUpdate;
        private Guid Id = Guid.NewGuid();

        public GameServiceTest()
        {
            GameRepo.Setup(x => x.Games.GetAll()).Returns(new List<Game>
           {
                new Game (),
                new Game (),
                new Game ()
           });
            GameRepo.Setup(x => x.Games.Create(It.IsAny<Game>())).Callback(() => _games.Add(It.IsAny<Game>()));
            GameRepo.Setup(x => x.Games.Get(Id)).Returns(new Game { Id = Id, Name = "game1" });
            GameRepo.Setup(x => x.Games.Update(It.IsAny<Game>())).Callback(() => _boolUpdate = true);
        }

        [Fact]
        public void GetAllGame_3Games_3Games()
        {
            var countGames = GameService.GetAllGame().Count();

            Xunit.Assert.Equal(3, countGames);
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

            Xunit.Assert.Equal(2, countGames);
        }

        [Fact]
        public void EditGame_GameDTO_updateDescription()
        {
            var testGame = new GameDTO();

            GameService.EditGame(testGame);

            Xunit.Assert.True(_boolUpdate);
        }
       
            
    }
}
