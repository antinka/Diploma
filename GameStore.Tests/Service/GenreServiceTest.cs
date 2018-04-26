using AutoMapper;
using GameStore.BLL.Exeption;
using GameStore.BLL.Service;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using log4net;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.Infrastructure.Mapper;
using Xunit;

namespace GameStore.Tests.Service
{
    public class GenreServiceTest
    {
        private readonly Mock<IUnitOfWork> _uow;
        private readonly GenreService _sut;
        private readonly IMapper _mapper;

        private readonly Genre _fakeGenre;
        private readonly Guid _fakeGenreId;
        private readonly List<Genre> _fakeGenres;

        public GenreServiceTest()
        {
            _uow = new Mock<IUnitOfWork>();
            var log = new Mock<ILog>();
            _mapper = MapperConfigUi.GetMapper().CreateMapper();
            _sut = new GenreService(_uow.Object, _mapper);

            _fakeGenreId = Guid.NewGuid();

            _fakeGenre = new Genre
            {
                Id = _fakeGenreId,
                Name = "genre1"
            };

            _fakeGenres = new List<Genre>()
            {
                _fakeGenre,

                new Genre()
                {
                    Id = new Guid(),
                    Name ="genre2"
                }
            };
        }

        [Fact]
        public void GetAllGenre_AllGenresReturned()
        {
            _uow.Setup(uow => uow.Genres.GetAll()).Returns(_fakeGenres);

            var resultGames = _sut.GetAll();

            Assert.Equal(resultGames.Count(), _fakeGenres.Count);
        }

        [Fact]
        public void GetGenreById_ExistedGameId_GameReturned()
        {
            _uow.Setup(uow => uow.Genres.GetById(_fakeGenreId)).Returns(_fakeGenre);

            var resultGame = _sut.GetById(_fakeGenreId);

            Assert.True(resultGame.Id == _fakeGenreId);
        }

        [Fact]
        public void GetGenreById_NotExistedGameKey_ExeptionEntityNotFound()
        {
            _uow.Setup(uow => uow.Genres.GetById(_fakeGenreId)).Returns(null as Genre);

            Assert.Throws<EntityNotFound>(() => _sut.GetById(_fakeGenreId));
        }
    }
}
