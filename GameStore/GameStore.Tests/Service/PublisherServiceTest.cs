using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Exeption;
using GameStore.BLL.Service;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using GameStore.Infrastructure.Mapper;
using log4net;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GameStore.Tests.Service
{
    public class PublisherServiceTest
    {
        private readonly Mock<IUnitOfWork> _uow;
        private readonly PublisherService _sut;
        private readonly IMapper _mapper;

        private readonly Publisher _fakePublisher;
        private readonly List<Publisher> _fakePublishers;
        private readonly string _fakePublisherName;
        private readonly Guid _fakePublisherId;

        public PublisherServiceTest()
        {
            _uow = new Mock<IUnitOfWork>();
            var log = new Mock<ILog>();
            _mapper = MapperConfigUi.GetMapper().CreateMapper();
            _sut = new PublisherService(_uow.Object, _mapper, log.Object);

            _fakePublisherName = "publisher1";
            _fakePublisherId = Guid.NewGuid();

            _fakePublisher = new Publisher
            {
                Id = _fakePublisherId,
                Name = _fakePublisherName
            };

            _fakePublishers = new List<Publisher>()
            {
                _fakePublisher
            };
        }

        [Fact]
        public void GetAllPublishers_AllPublishersReturned()
        {
            _uow.Setup(uow => uow.Publishers.GetAll()).Returns(_fakePublishers);

            var resultPublishers = _sut.GetAll();

            Assert.Equal(resultPublishers.Count(), _fakePublishers.Count);
        }

        [Fact]
        public void GetPublisherByName_ExistedPublisherName_PublisherReturned()
        {
            _uow.Setup(uow => uow.Publishers.Get(It.IsAny<Func<Publisher, bool>>())).Returns(_fakePublishers);

            var resultPublisher = _sut.GetByName(_fakePublisherName);

            Assert.True(resultPublisher.Name == _fakePublisherName);
        }

        [Fact]
        public void GetPublisherByName_NotExistedPublisherName_ExeptionEntityNotFound()
        {
            _uow.Setup(uow => uow.Publishers.Get(It.IsAny<Func<Publisher, bool>>())).Returns(new List<Publisher>());

            Assert.Throws<EntityNotFound>(() => _sut.GetByName(_fakePublisherName));
        }

        [Fact]
        public void AddNewPublisher_PublisherWithUniqueName_Verifiable()
        {
            var fakePublisherDTO = new PublisherDTO() {Id = Guid.NewGuid(), Name = "publisherUniqueName"};
            var fakePublisher = _mapper.Map<Publisher>(fakePublisherDTO);

            _uow.Setup(uow => uow.Publishers.Get(It.IsAny<Func<Publisher, bool>>())).Returns(new List<Publisher>());
            _uow.Setup(uow => uow.Publishers.Create(fakePublisher)).Verifiable();

            _sut.AddNew(fakePublisherDTO);

            _uow.Verify(uow => uow.Publishers.Create(It.IsAny<Publisher>()), Times.Once);
        }

        [Fact]
        public void AddNewPublisher_PublisherWithoutUniqueName_ReturnedFalseAddNewPublishe()
        {
            var fakePublisherDTO = new PublisherDTO() {Id = Guid.NewGuid(), Name = _fakePublisherName};

            _uow.Setup(uow => uow.Publishers.Get(It.IsAny<Func<Publisher, bool>>())).Returns(_fakePublishers);

            Assert.False(_sut.AddNew(fakePublisherDTO));
        }

        [Fact]
        public void UpdatePublisher_Publisher_Verifiable()
        {
            var fakePublisherDTO = new PublisherDTO() {Id = _fakePublisherId, Name = "test"};
            var fakePublisher = _mapper.Map<Publisher>(fakePublisherDTO);


            _uow.Setup(uow => uow.Publishers.GetById(_fakePublisherId)).Returns(fakePublisher);

            _uow.Setup(uow => uow.Publishers.Update(fakePublisher)).Verifiable();

            _sut.Update(fakePublisherDTO);

            _uow.Verify(uow => uow.Publishers.Update(It.IsAny<Publisher>()), Times.Once);
        }

        [Fact]
        public void UpdatePublisher_NotExistPublisherName_EntityNotFound()
        {
            _uow.Setup(uow => uow.Publishers.GetById(_fakePublisherId)).Returns(null as Publisher);

            Assert.Throws<EntityNotFound>(() => _sut.Update(new PublisherDTO()));
        }

        [Fact]
        public void DeletePublisher_NotExistedPublisherName__ExeptionEntityNotFound()
        {
            var notExistPublisherId = Guid.NewGuid();

            _uow.Setup(uow => uow.Publishers.GetById(notExistPublisherId)).Returns(null as Publisher);
            _uow.Setup(uow => uow.Publishers.Delete(notExistPublisherId));

            Assert.Throws<EntityNotFound>(() => _sut.Delete(notExistPublisherId));
        }

        [Fact]
        public void DeletePublisher_ExistedPublisherName__Verifiable()
        {
            _uow.Setup(uow => uow.Publishers.GetById(_fakePublisherId)).Returns(_fakePublisher);
            _uow.Setup(uow => uow.Publishers.Delete(_fakePublisherId)).Verifiable();

            _sut.Delete(_fakePublisherId);

            _uow.Verify(uow => uow.Publishers.Delete(It.IsAny<Guid>()), Times.Once);
        }
    }
}
