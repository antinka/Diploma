using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.Controllers;
using GameStore.ViewModels;
using Moq;
using Xunit;
using System.Web.Mvc;
using GameStore.Infrastructure.Mapper;

namespace GameStore.Tests.Controllers
{
    public class PublisherControllerTest
    {
        private readonly Mock<IPublisherService> _publisherService;
        private readonly IMapper _mapper;
        private readonly PublisherController _sut;

        private readonly string _fakePublisherName;

        public PublisherControllerTest()
        {
            _mapper = MapperConfigUi.GetMapper().CreateMapper();
            _publisherService = new Mock<IPublisherService>();
            _sut = new PublisherController(_publisherService.Object, _mapper);

            _fakePublisherName = "test";
        }

        [Fact]
        public void New_ValidPublisherViewModel_Verifiable()
        {
            var fakePublisherViewModel = new PublisherViewModel() { Name = "test", Description = "test", HomePage = "test" };
            var fakePublisherDTO = _mapper.Map<PublisherDTO>(fakePublisherViewModel);

            _publisherService.Setup(service => service.AddNew(fakePublisherDTO)).Verifiable();

            _sut.New(fakePublisherViewModel);

            _publisherService.Verify(s => s.AddNew(It.IsAny<PublisherDTO>()), Times.Once);
        }

        [Fact]
        public void New_InalidPublisherViewModel_ReturnView()
        {
            var fakePublisherViewModel = new PublisherViewModel() { Name = "test" };
            _sut.ModelState.Add("testError", new ModelState());
            _sut.ModelState.AddModelError("testError", "test");

            var res = _sut.New(fakePublisherViewModel);

            Assert.Equal(typeof(ViewResult), res.GetType());
        }

        [Fact]
        public void Get_PublisherName_Verifiable()
        {
            _publisherService.Setup(service => service.GetByName(_fakePublisherName)).Verifiable();

            _sut.Get(_fakePublisherName);

            _publisherService.Verify(s => s.GetByName(It.IsAny<string>()), Times.Once);
        }
    }
}
