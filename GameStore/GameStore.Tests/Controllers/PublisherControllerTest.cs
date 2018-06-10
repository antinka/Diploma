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
            _sut = new PublisherController(_publisherService.Object, _mapper, null);

            _fakePublisherName = "test";
        }

        [Fact]
        public void New_ValidPublisherViewModel_AddNewCalled()
        {
            var fakePublisherViewModel = new PublisherViewModel() { Name = "test", DescriptionEn = "test", HomePage = "test" };
            var fakePublisherDTO = _mapper.Map<ExtendPublisherDTO>(fakePublisherViewModel);

            _publisherService.Setup(service => service.IsUniqueName(It.IsAny<ExtendPublisherDTO>())).Returns(true);
            _publisherService.Setup(service => service.AddNew(fakePublisherDTO));

            _sut.New(fakePublisherViewModel);

            _publisherService.Verify(s => s.AddNew(It.IsAny<ExtendPublisherDTO>()), Times.Once);
        }

        [Fact]
        public void New_InvalidPublisherViewModel_ReturnViewResult()
        {
            var fakePublisherViewModel = new PublisherViewModel() { Name = "test" };
            _sut.ModelState.Add("testError", new ModelState());
            _sut.ModelState.AddModelError("testError", "test");

            var res = _sut.New(fakePublisherViewModel);

            Assert.IsType<ViewResult>(res);
        }

        [Fact]
        public void Get_PublisherName_GetByNameCalled()
        {
            _publisherService.Setup(service => service.GetByName(_fakePublisherName));

            _sut.Get(_fakePublisherName);

            _publisherService.Verify(s => s.GetByName(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void Update_ValidUpdatePublisher_UpdateCalled()
        {
            var fakePublisherViewModel = new PublisherViewModel() { Name = "test" };
            var fakePublisherDTO = _mapper.Map<ExtendPublisherDTO>(fakePublisherViewModel);

            _publisherService.Setup(service => service.IsUniqueName(It.IsAny<ExtendPublisherDTO>())).Returns(true);
            _publisherService.Setup(service => service.Update(fakePublisherDTO));

            _sut.Update(fakePublisherViewModel);

            _publisherService.Verify(s => s.Update(It.IsAny<ExtendPublisherDTO>()), Times.Once);
        }

        [Fact]
        public void Update_InvalidUpdatePublisher_ReturnViewResult()
        {
            var fakePublisherViewModel = new PublisherViewModel();
            _sut.ModelState.Add("testError", new ModelState());
            _sut.ModelState.AddModelError("testError", "test");

            var res = _sut.Update(fakePublisherViewModel);

            Assert.IsType<ViewResult>(res);
        }

        [Fact]
        public void Update_companyName_ReturnView()
        {
            var res = _sut.Update(_fakePublisherName);

            Assert.IsType<ViewResult>(res);
        }

        [Fact]
        public void Remove_PublisherId_DeleteCalled()
        {
            var fakePublisherId = Guid.NewGuid();
            _publisherService.Setup(service => service.Delete(fakePublisherId));

            _sut.Remove(fakePublisherId);

            _publisherService.Verify(s => s.Delete(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void GetAll_ReturnedViewResult()
        {
            var fakePublishersDto = new List<PublisherDTO>()
            {
                new PublisherDTO() { Name = "test1", Description = "test", HomePage = "test" },
                new PublisherDTO() { Name = "test2", Description = "test", HomePage = "test" },
            };

            _publisherService.Setup(service => service.GetAll()).Returns(fakePublishersDto);

            var res = _sut.GetAll();

            Assert.IsType<ViewResult>(res);
        }

        [Fact]
        public void New_ReturnViewResult()
        {
            var res = _sut.New();

            Assert.IsType<ViewResult>(res);
        }
    }
}
