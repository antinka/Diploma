﻿
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
                new Game (),
                new Game (),
                new Game ()
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
        public void AddNewGame_2GamesWithUniqKey_2Games()
        {
            GameDTO game1 = new GameDTO();
            game1.Key = "1";
            GameDTO game2 = new GameDTO();
            game2.Key = "2";
            gameService.AddNewGame(game1);
            gameService.AddNewGame(game2);

            gameRepo.Setup(x => x.Games.GetAll()) .Returns(games);
            var countGames = gameService.GetAllGame().Count();

            Xunit.Assert.Equal(2, countGames);
        }

        [Fact]
        public void EditGame_GameDTO_updateDescription()
        {
            GameDTO testGame = new GameDTO();

            gameService.EditGame(testGame);

            Xunit.Assert.True(boolUpdate);
        }
       
            
    }
}
