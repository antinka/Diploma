
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
        
        static Mock<IUnitOfWork> gameRepo = new Mock<IUnitOfWork>();
        static GameService gameService = new GameService(gameRepo.Object);
     
        List<Game> games = new List<Game>();
        bool boolDelete = false;
        bool boolUpdate = false;
        public Guid id = Guid.NewGuid();

        public GameServiceTest()
        {
            gameRepo.Setup(x => x.Games.GetAll()).Returns(new List<Game>
           {
                new Game ("name1","description1","key1"),
                new Game ("name2","description2","key2"),
                new Game ("name3","description3","key3")
           });
            gameRepo.Setup(x => x.Games.Create(It.IsAny<Game>())).Callback(() => games.Add(It.IsAny<Game>()));
            gameRepo.Setup(x => x.Games.Delete(It.IsAny<Guid>())).Callback(() => boolDelete = true);
            gameRepo.Setup(x => x.Games.Get(id)).Returns(new Game { Id = id, Name = "game1" });
            gameRepo.Setup(x => x.Games.Update(It.IsAny<Game>())).Callback(() => boolUpdate = true);
        }

        [Fact]
        public void GetAllGame_3Games_3Games()
        {
            var countGames = gameService.GetAllGame().Count();

            Xunit.Assert.Equal(3, countGames);
        }

        [Fact]
        public void AddNewGame_2GamesOneWithNotUniqKey_1Games()
        {
            GameDTO game1 = new GameDTO("name","description","key1");
            GameDTO game2 = new GameDTO("name", "description", "key");
            gameService.AddNewGame(game1);
            gameService.AddNewGame(game2);

            gameRepo.Setup(x => x.Games.GetAll()) .Returns(games);
            var countGames = gameService.GetAllGame().Count();

            Xunit.Assert.Equal(1, countGames);
        }

        [Fact]
        public void DeleteGame_Id_True()
        {
            gameService.DeleteGame(id);

            Xunit.Assert.True(boolDelete);
        }

        [Fact]
        public void EditGame_GameDTO_updateDescription()
        {
            GameDTO testGame = new GameDTO("name", "description", "key1");

            gameService.EditGame(testGame);

            Xunit.Assert.True(boolUpdate);
        }
       
            
    }
}
