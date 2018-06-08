using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Service;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using GameStore.Web.Infrastructure.Mapper;
using log4net;
using Moq;
using GameStore.BLL.CustomExeption;
using Xunit;

namespace GameStore.Tests.Service
{
    public class GenreServiceTest
    {
        private readonly Mock<IUnitOfWork> _uow;
        private readonly GenreService _sut;
        private readonly IMapper _mapper;

        private readonly string _fakeGenreName;
        private readonly Genre _fakeGenre;
        private readonly Guid _fakeGenreId;
        private readonly List<Genre> _fakeGenres;

        public GenreServiceTest()
        {
            _uow = new Mock<IUnitOfWork>();
            var log = new Mock<ILog>();
            _mapper = MapperConfigUi.GetMapper().CreateMapper();
            _sut = new GenreService(_uow.Object, _mapper, log.Object);

            _fakeGenreName = "genre1";
            _fakeGenreId = Guid.NewGuid();

            _fakeGenre = new Genre
            {
                Id = _fakeGenreId,
                NameEn = _fakeGenreName
            };

            _fakeGenres = new List<Genre>()
            {
                _fakeGenre,

                new Genre()
                {
                    Id = new Guid(),
                    NameEn = "genre2"
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

        [Fact]
        public void GetGenreByName_ExistedGenreName_GenreReturned()
        {
            var fakeGenres = new List<Genre>()
            {
                _fakeGenre
            };

            _uow.Setup(uow => uow.Genres.Get(It.IsAny<Func<Genre, bool>>())).Returns(fakeGenres);

            var result = _sut.GetByName(_fakeGenreName);

            Assert.True(result.NameEn == _fakeGenreName);
        }

        [Fact]
        public void GetGenreByName_NotExistedGenreNam_ExeptionEntityNotFound()
        {
            _uow.Setup(uow => uow.Genres.Get(It.IsAny<Func<Genre, bool>>())).Returns(new List<Genre>());

            Assert.Throws<EntityNotFound>(() => _sut.GetByName(_fakeGenreName));
        }

        [Fact]
        public void AddNewGenre_GenreWithUniqueName_Verifiable()
        {
            var fakeGenreDTO = new ExtendGenreDTO() { Id = Guid.NewGuid(), NameEn = "uniqueName" };
            var fakeGenre = _mapper.Map<Genre>(fakeGenreDTO);

            _uow.Setup(uow => uow.Genres.Get(It.IsAny<Func<Genre, bool>>())).Returns(new List<Genre>());
            _uow.Setup(uow => uow.Genres.Create(fakeGenre)).Verifiable();

            _sut.AddNew(fakeGenreDTO);

            _uow.Verify(uow => uow.Genres.Create(It.IsAny<Genre>()), Times.Once);
        }

        [Fact]
        public void UpdateGenre_Genre_GenreUpdated()
        {
            var fakeGenreDTO = _mapper.Map<ExtendGenreDTO>(_fakeGenre);

            _uow.Setup(uow => uow.Genres.GetById(_fakeGenreId)).Returns(_fakeGenre);
            _uow.Setup(uow => uow.Genres.Update(_fakeGenre)).Verifiable();

            _sut.Update(fakeGenreDTO);

            _uow.Verify(uow => uow.Genres.Update(It.IsAny<Genre>()), Times.Once);
        }

        [Fact]
        public void UpdateGenre_NotExistGenreName_EntityNotFound()
        {
            var fakeGenreId = Guid.NewGuid();

            _uow.Setup(uow => uow.Genres.GetById(fakeGenreId)).Returns(null as Genre);

            Assert.Throws<EntityNotFound>(() => _sut.Update(new ExtendGenreDTO()));
        }

        [Fact]
        public void DeleteGenre_NotExistedGenreName__ExeptionEntityNotFound()
        {
            var notExistGenreId = Guid.NewGuid();

            _uow.Setup(uow => uow.Genres.GetById(notExistGenreId)).Returns(null as Genre);
            _uow.Setup(uow => uow.Genres.Delete(notExistGenreId));

            Assert.Throws<EntityNotFound>(() => _sut.Delete(notExistGenreId));
        }

        [Fact]
        public void DeleteGenre_ExistedGenreName__Verifiable()
        {
            _uow.Setup(uow => uow.Genres.GetById(_fakeGenreId)).Returns(_fakeGenre);
            _uow.Setup(uow => uow.Genres.Delete(_fakeGenreId)).Verifiable();

            _sut.Delete(_fakeGenreId);

            _uow.Verify(uow => uow.Genres.Delete(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void IsUniqueEnName_UniqueName_True()
        {
            var genre = new ExtendGenreDTO() { Id = Guid.NewGuid(), NameEn = _fakeGenreName };
            _uow.Setup(uow => uow.Genres.Get(It.IsAny<Func<Genre, bool>>())).Returns(new List<Genre>());

            var res = _sut.IsUniqueEnName(genre);

            Assert.True(res);
        }

        [Fact]
        public void IsUniqueEnName_NotUniqueName_False()
        {
            var genre = new ExtendGenreDTO() { Id = Guid.NewGuid(), NameEn = _fakeGenreName };
            _uow.Setup(uow => uow.Genres.Get(It.IsAny<Func<Genre, bool>>())).Returns(new List<Genre>() { _fakeGenre });

            var res = _sut.IsUniqueEnName(genre);

            Assert.False(res);
        }

        [Fact]
        public void IsPossibleRelation_GenreDTOWithPossibleRelation_True()
        {
            Guid firstFakeId = Guid.NewGuid();
            var newGenre = new ExtendGenreDTO() { Id = Guid.NewGuid(), NameEn = _fakeGenreName, ParentGenreId = firstFakeId };
            _uow.Setup(uow => uow.Genres.Get(It.IsAny<Func<Genre, bool>>())).Returns(new List<Genre>());

            var res = _sut.IsPossibleRelation(newGenre);

            Assert.True(res);
        }

        [Fact]
        public void IsPossibleRelation_GenreDTOWithoutPossibleRelation_False()
        {
            Guid firstFakeId = Guid.NewGuid(), secondFakeId = Guid.NewGuid();
            var newGenre = new ExtendGenreDTO() { Id = firstFakeId, NameEn = _fakeGenreName, ParentGenreId = secondFakeId };
            var existGenre = new Genre() { Id = secondFakeId, NameEn = _fakeGenreName, ParentGenreId = firstFakeId };
            _uow.Setup(uow => uow.Genres.Get(It.IsAny<Func<Genre, bool>>())).Returns(new List<Genre>() { existGenre });

            var res = _sut.IsPossibleRelation(newGenre);

            Assert.False(res);
        }
    }
}
