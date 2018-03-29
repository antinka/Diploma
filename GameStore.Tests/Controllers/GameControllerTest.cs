using GameStore.App_Start;
using GameStore.BAL.DTO;
using GameStore.BAL.Interfaces;
using GameStore.Controllers;
using GameStore.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GameStore.Tests.Controllers
{
    public class GameControllerTest
    {
        static Mock<IGameService> gameRepo = new Mock<IGameService>();
        List<GameDTO> games = new List<GameDTO>();
        public Guid id = Guid.NewGuid();
        bool boolDelete = false;
        bool boolUpdate = false;
        bool boolGet = false;
        GameController gameController = new GameController(gameRepo.Object);

        public GameControllerTest()
        {
            AutoMapper.Mapper.Reset();
            DTOToViewModel.Initialize();
            gameRepo.Setup(x => x.AddNewGame(It.IsAny<GameDTO>())).Callback(() => games.Add(It.IsAny<GameDTO>()));
            gameRepo.Setup(x => x.EditGame(It.IsAny<GameDTO>())).Callback(() => boolUpdate = true);
            gameRepo.Setup(x => x.DeleteGame(It.IsAny<Guid>())).Callback(() => boolDelete = true);
            gameRepo.Setup(x => x.GetGame(It.IsAny<Guid>())).Callback(() => boolGet = true);
        }

        [Fact]
        public  void NewGame_AddNewGame_NewGameInList()
        {
            GameViewModel game = new GameViewModel();

            var result = gameController.New(game);
            int countNewGames = games.Count();

            Assert.Equal(1, countNewGames);
        }

        [Fact]
        public void UpdateGame_GameDTO_boolUpdateTrue()
        {
            GameViewModel game = new GameViewModel("name", "description", "key1");

            gameController.Update(game);

            Xunit.Assert.True(boolUpdate);
        }

        [Fact]
        public void DeleteGame_Id_boolDeleteTrue()
        {
            gameController.Remove(id);

            Xunit.Assert.True(boolDelete);
        }

        [Fact]
        public void GetGame_Id_boolGetTrue()
        {
            gameController.GetGameById(id);

            Xunit.Assert.True(boolGet);
        }
    }
}
