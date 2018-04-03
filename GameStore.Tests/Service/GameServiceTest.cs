using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Service;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using log4net;
using Moq;
using System;
using Xunit;

namespace GameStore.Tests.Service
{
    public class GameServiceTest
    {
        private readonly Mock<IUnitOfWork> _uow;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ILog> log;
        private readonly GameService _sut;

        private readonly Guid _id = Guid.NewGuid();

        public GameServiceTest()
        {
            _uow = new Mock<IUnitOfWork>();
            _mapper = new Mock<IMapper>();
            log = new Mock<ILog>();
            _sut = new GameService(_uow.Object, _mapper.Object, log.Object);
        }

        [Fact]
        public void GetAllGame_GetAllGames_VerifyAll()
        {
            _uow.Setup(x => x.Games.GetAll());

            _sut.GetAllGame();

            _uow.VerifyAll();
        }

        [Fact]
        public void GetGame_GameDTO_VerifyAll()
        {
            _uow.Setup(x => x.Games.GetById(_id));

            _sut.GetGame(_id);

            _uow.VerifyAll();
        }
        [Fact]
        public void AddNewGame_Game_VerifyAll()
        {
            _uow.Setup(x => x.Games.Create(It.IsAny<Game>()));

            _sut.AddNewGame(new GameDTO());

            _uow.VerifyAll();
        }

        [Fact]
        public void UpdateGame_GameDTO_AddNewGame_Game_TrueIsUpdate()
        {
            _uow.Setup(x => x.Games.Update(It.IsAny<Game>()));

            _sut.UpdateGame(new GameDTO());

            _uow.VerifyAll();
        }
    }
}
