using System;
using System.Collections.Generic;
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
        public void New_InvalidPublisherViewModel_ReturnView()
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

        [Fact]
        public void Update_ValidUpdatePublisher_HttpStatusCodeOK()
        {
            var fakePublisherViewModel = new PublisherViewModel() { Name = "test"};
            var fakePublisherDTO = _mapper.Map<PublisherDTO>(fakePublisherViewModel);

            _publisherService.Setup(service => service.Update(fakePublisherDTO)).Verifiable();

            _sut.Update(fakePublisherViewModel);

            _publisherService.Verify(s => s.Update(It.IsAny<PublisherDTO>()), Times.Once);
        }

        [Fact]
        public void Update_InvalidUpdatePublisher_HttpStatusCodeOK()
        {
            var fakePublisherViewModel = new PublisherViewModel();
            _sut.ModelState.Add("testError", new ModelState());
            _sut.ModelState.AddModelError("testError", "test");

            var res = _sut.Update(fakePublisherViewModel);

            Assert.Equal(typeof(ViewResult), res.GetType());
        }

        [Fact]
        public void Remove_PublisherId_HttpStatusCodeOK()
        {
            var fakePublisherId = Guid.NewGuid();
            _publisherService.Setup(service => service.Delete(fakePublisherId));

            _sut.Remove(fakePublisherId);

            _publisherService.Verify(s => s.Delete(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void GetAll_ReturnedView()
        {
            var fakePublishersDto = new List<PublisherDTO>()
            {
                new PublisherDTO() { Name = "test1", Description = "test", HomePage = "test" },
                new PublisherDTO() { Name = "test2", Description = "test", HomePage = "test" },
            };

            _publisherService.Setup(service => service.GetAll()).Returns(fakePublishersDto);

            var res = _sut.GetAll();

            Assert.Equal(typeof(ViewResult), res.GetType());
        }
    }
}
