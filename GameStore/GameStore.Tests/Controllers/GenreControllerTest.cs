using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.Controllers;
using GameStore.Infrastructure.Mapper;
using GameStore.ViewModels;
using Moq;
using Xunit;

namespace GameStore.Tests.Controllers
{
    public class GenreControllerTest
    {
        private readonly Mock<IGenreService> _genreService;
        private readonly IMapper _mapper;
        private readonly GenreController _sut;

        private readonly string _fakeGenreName;

        public GenreControllerTest()
        {
            _mapper = MapperConfigUi.GetMapper().CreateMapper();
            _genreService = new Mock<IGenreService>();
            _sut = new GenreController(_genreService.Object, _mapper);

            _fakeGenreName = "test";
        }

        [Fact]
        public void New_ValidGenreViewModel_Verifiable()
        {
            var fakeGenreViewModel = new GenreViewModel() { Name = "test" };
            var fakeGenreDTO = _mapper.Map<GenreDTO>(fakeGenreViewModel);

            _genreService.Setup(service => service.AddNew(fakeGenreDTO)).Verifiable();

            _sut.New(fakeGenreViewModel);

            _genreService.Verify(s => s.AddNew(It.IsAny<GenreDTO>()), Times.Once);
        }

        [Fact]
        public void New_InvalidGenreViewModel_ReturnViewResult()
        {
            var fakeGenreViewModel = new GenreViewModel() { Name = "test" };
            _sut.ModelState.Add("testError", new ModelState());
            _sut.ModelState.AddModelError("testError", "test");

            var res = _sut.New(fakeGenreViewModel);

            Assert.Equal(typeof(ViewResult), res.GetType());
        }

        [Fact]
        public void Get_GenreName_Verifiable()
        {
            _genreService.Setup(service => service.GetByName(_fakeGenreName)).Verifiable();

            _sut.Get(_fakeGenreName);

            _genreService.Verify(s => s.GetByName(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void Update_ValidGenre_Verifiable()
        {
            var fakeGenreViewModel = new GenreViewModel() { Name = "test" };
            var fakeGenreDTO = _mapper.Map<GenreDTO>(fakeGenreViewModel);

            _genreService.Setup(service => service.Update(fakeGenreDTO)).Verifiable();

            _sut.Update(fakeGenreViewModel);

            _genreService.Verify(s => s.Update(It.IsAny<GenreDTO>()), Times.Once);
        }

        [Fact]
        public void Update_InvalidGenre_ReturnViewResult()
        {
            var fakeGenreViewModel = new GenreViewModel();
            _sut.ModelState.Add("testError", new ModelState());
            _sut.ModelState.AddModelError("testError", "test");

            var res = _sut.Update(fakeGenreViewModel);

            Assert.Equal(typeof(ViewResult), res.GetType());
        }

        [Fact]
        public void Update_GenreName_ReturnedView()
        {
            var fakeGenre = new GenreDTO() {Id = Guid.NewGuid(), Name = _fakeGenreName};

            _genreService.Setup(service => service.GetByName(_fakeGenreName)).Returns(fakeGenre);
            _genreService.Setup(service => service.GetAll()).Returns(new List<GenreDTO>());

            var res = _sut.Update(_fakeGenreName);

            Assert.Equal(typeof(ViewResult), res.GetType());
        }

        [Fact]
        public void Remove_GenreId_Verifiable()
        {
            var fakeGenreId = Guid.NewGuid();
            _genreService.Setup(service => service.Delete(fakeGenreId)).Verifiable();

            _sut.Remove(fakeGenreId);

            _genreService.Verify(s => s.Delete(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void GetAll_ReturnedViewResult()
        {
            var fakeGenreDTO = new List<GenreDTO>()
            {
                new GenreDTO() { Name = "test1"}
            };

            _genreService.Setup(service => service.GetAll()).Returns(fakeGenreDTO);

            var res = _sut.GetAll();

            Assert.Equal(typeof(ViewResult), res.GetType());
        }

        [Fact]
        public void New_ReturnViewResult()
        {
            var res = _sut.New();

            Assert.Equal(typeof(ViewResult), res.GetType());
        }
    }
}
