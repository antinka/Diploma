using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.Web.Controllers;
using GameStore.Web.Infrastructure.Mapper;
using GameStore.Web.ViewModels;
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
            _sut = new GenreController(_genreService.Object, _mapper, null);

            _fakeGenreName = "test";
        }

        [Fact]
        public void New_ValidGenreViewModel_AddNewCallled()
        {
            var fakeGenreViewModel = new GenreViewModel() { NameEn = "test" };
            var fakeGenreDTO = _mapper.Map<ExtendGenreDTO>(fakeGenreViewModel);

            _genreService.Setup(service => service.IsUniqueEnName(It.IsAny<ExtendGenreDTO>())).Returns(true);
            _genreService.Setup(service => service.AddNew(fakeGenreDTO));

            _sut.New(fakeGenreViewModel);

            _genreService.Verify(s => s.AddNew(It.IsAny<ExtendGenreDTO>()), Times.Once);
        }

        [Fact]
        public void New_InvalidGenreViewModel_ReturnViewResult()
        {
            var fakeGenreViewModel = new GenreViewModel() { NameEn = "test" };
            _sut.ModelState.Add("testError", new ModelState());
            _sut.ModelState.AddModelError("testError", "test");

            var res = _sut.New(fakeGenreViewModel);

            Assert.Equal(typeof(ViewResult), res.GetType());
        }

        [Fact]
        public void Get_GenreName_GetByNameCalled()
        {
            _genreService.Setup(service => service.GetByName(_fakeGenreName));

            _sut.Get(_fakeGenreName);

            _genreService.Verify(s => s.GetByName(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void Update_ValidGenre_UpdateMethdCalled()
        {
            var fakeGenreViewModel = new GenreViewModel() { NameEn = "test" };
            var fakeGenreDTO = _mapper.Map<ExtendGenreDTO>(fakeGenreViewModel);

            _genreService.Setup(service => service.IsUniqueEnName(It.IsAny<ExtendGenreDTO>())).Returns(true);
            _genreService.Setup(service => service.Update(fakeGenreDTO));

            _sut.Update(fakeGenreViewModel);

            _genreService.Verify(s => s.Update(It.IsAny<ExtendGenreDTO>()), Times.Once);
        }

        [Fact]
        public void Update_InvalidGenre_ReturnViewResult()
        {
            var fakeGenreViewModel = new GenreViewModel();
            _sut.ModelState.Add("testError", new ModelState());
            _sut.ModelState.AddModelError("testError", "test");

            var res = _sut.Update(fakeGenreViewModel);

            Assert.IsType<ViewResult>(res);
        }

        [Fact]
        public void Update_GenreName_ReturnedView()
        {
            var fakeGenre = new ExtendGenreDTO() { Id = Guid.NewGuid(), NameEn = _fakeGenreName };

            _genreService.Setup(service => service.GetByName(_fakeGenreName)).Returns(fakeGenre);
            _genreService.Setup(service => service.GetAll()).Returns(new List<GenreDTO>());

            var res = _sut.Update(_fakeGenreName);

            Assert.IsType<ViewResult>(res);
        }

        [Fact]
        public void Remove_GenreId_DeleteCalled()
        {
            var fakeGenreId = Guid.NewGuid();
            _genreService.Setup(service => service.Delete(fakeGenreId));

            _sut.Remove(fakeGenreId);

            _genreService.Verify(s => s.Delete(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void GetAll_ReturnedViewResult()
        {
            var fakeGenreDTO = new List<GenreDTO>()
            {
                new GenreDTO() { Name = "test1" }
            };

            _genreService.Setup(service => service.GetAll()).Returns(fakeGenreDTO);

            var res = _sut.GetAll();

            Assert.IsType<ViewResult>(res);
        }

        [Fact]
        public void New_ReturnViewResult()
        {
            var res = _sut.New();

            Assert.IsType<ViewResult>(res);
        }

        [Fact]
        public void New_GenreWithoutUnickName_IsUniqueEnNameCalled()
        {
            var fakeGenreViewModel = new GenreViewModel() { NameEn = "test", };
            _genreService.Setup(service => service.IsUniqueEnName(It.IsAny<ExtendGenreDTO>())).Returns(false);

            _sut.New(fakeGenreViewModel);

            _genreService.Verify(s => s.IsUniqueEnName(It.IsAny<ExtendGenreDTO>()), Times.Once);
        }

        [Fact]
        public void Update_GenreWithoutUnickName_IsUniqueEnNameCalled()
        {
            var fakeGenreViewModel = new GenreViewModel() { NameEn = "test" };
            _genreService.Setup(service => service.IsUniqueEnName(It.IsAny<ExtendGenreDTO>())).Returns(false);

            _sut.Update(fakeGenreViewModel);

            _genreService.Verify(s => s.IsUniqueEnName(It.IsAny<ExtendGenreDTO>()), Times.Once);
        }
    }
}
