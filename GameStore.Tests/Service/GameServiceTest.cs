using AutoMapper;
using GameStore.BAL.DTO;
using GameStore.BAL.Service;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using log4net;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace GameStore.Tests.Service
{
    public class GameServiceTest
    {
        private static readonly Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
        private static readonly Mock<IMapper> mapper = new Mock<IMapper>();
        private static readonly Mock<ILog> log = new Mock<ILog>();

        private static readonly GameService GameService = new GameService(uow.Object, mapper.Object, log.Object);

        private readonly List<Game> _games = new List<Game>();
        private bool _isUpdate, _isGetById, _isCreate,_isGetAll;
        private readonly Guid _id = Guid.NewGuid();

        public GameServiceTest()
        {
            uow.Setup(x => x.Games.GetAll()).Callback(() => _isGetAll = true);
            uow.Setup(x => x.Games.Create(It.IsAny<Game>())).Callback(() => _isCreate = true);
            uow.Setup(x => x.Games.GetById(_id)).Callback(() => _isGetById = true);
            uow.Setup(x => x.Games.Update(It.IsAny<Game>())).Callback(() => _isUpdate = true);
        }

        [Fact]
        public void GetAllGame_GetAllGames_TrueIsGetAll()
        {
            var games = GameService.GetAllGame();

            Assert.True(_isGetAll);
        }

        [Fact]
        public void AddNewGame_Game_TrueIsCreate()
        {
            var game1 = new GameDTO
            {
                Key = "123456654"
            };

            GameService.AddNewGame(game1);

            Assert.True(_isCreate);
        }

        [Fact]
        public void UpdateGame_GameDTO_AddNewGame_Game_TrueIsUpdate()
        {
            var testGame = new GameDTO();

            GameService.UpdateGame(testGame);

            Assert.True(_isUpdate);
        }

        [Fact]
        public void GetGame_GameDTO_TrueIsGetById()
        {
            GameService.GetGame(_id);

            Assert.True(_isGetById);
        }
    }
}
